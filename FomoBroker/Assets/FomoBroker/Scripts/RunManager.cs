using System.Collections.Generic;
using UnityEngine;

public class Runner {
    public Transform transform;
    public int baseIndex;

    public bool isRunning = false;
    public Vector3 targetPosition;
    public Animation anim;

    public List<Vector3> waypoints = new();
    public int waypointIndex = 0;

    public float runSpeed;
}

public class RunManager : MonoBehaviour {
    public GameObject dudePrefab;

    public Transform[] bases;
    public Transform[] roads;
    readonly float baseRadius = 10.0f;
    readonly List<Runner> runners = new();
    readonly int[] runnerCountAtBase = new int[3];

    private bool runnersAreRunning = true;
    public bool RunnersAreRunning { get => runnersAreRunning; }

    // Start is called before the first frame update
    void Start() {
        //SpawnDudes(new int[] { 30, 50, 10 });
    }

    public void SpawnDudes(int[] numForBase) {
        if(numForBase.Length != bases.Length) {
            Debug.LogError("numForBase length must match bases length");
            return;
        }
        for(int baseIndex = 0; baseIndex < numForBase.Length; baseIndex++) {
            runnerCountAtBase[baseIndex] = numForBase[baseIndex];
            Debug.Log("Spawn " + numForBase[baseIndex] + " runners at base " + baseIndex);
            Vector3 basePos = bases[baseIndex].position;
            for(int ii = 0; ii < numForBase[baseIndex]; ++ii) {
                GameObject go = GameObject.Instantiate(dudePrefab);
                Vector2 randomOffset = Random.insideUnitCircle * baseRadius;
                go.transform.position = basePos + new Vector3(randomOffset.x, 0, randomOffset.y);

                Runner runner = new() {
                    transform = go.transform,
                    baseIndex = baseIndex
                };

                runner.anim = runner.transform.GetComponent<Animation>();
                runner.anim.Play("Armature|idle");

                runner.runSpeed = Random.Range(25.0f, 35.0f);
                runner.anim["Armature|run"].speed = 4.0f * runner.runSpeed / 30.0f;
                runners.Add(runner);
            }
        }
    }

    const int BASE_COUNT = 3;


    public void Run(int[][] shiftCount) {
        if(RunnersAreRunning) return;

        List<Runner>[] runnersAtBase = new List<Runner>[BASE_COUNT];
        for(int ii = 0; ii < BASE_COUNT; ++ii) {
            runnersAtBase[ii] = new List<Runner>();
        }
        for(int ri = 0; ri < runners.Count; ri++) {
            Runner runner = runners[ri];
            runnersAtBase[runner.baseIndex].Add(runner);
        }

        for(int bi = 0; bi < BASE_COUNT; ++bi) {
            for(int si = 0; si < BASE_COUNT - 1; ++si) {
                int targetBaseIndex = (bi + si + 1) % BASE_COUNT;
                int roadIndex = ((bi + 1) % bases.Length == targetBaseIndex) ? bi : (bi + 2) % bases.Length;
                runnerCountAtBase[bi]              -= shiftCount[bi][si];
                runnerCountAtBase[targetBaseIndex] += shiftCount[bi][si];

                for(int ii = 0; ii < shiftCount[bi][si]; ++ii) {
                    int runnerIndex = Random.Range(0, runnersAtBase[bi].Count);
                    Runner runner = runnersAtBase[bi][runnerIndex];
                    Debug.Assert(runner.baseIndex == bi);
                    runnersAtBase[bi].RemoveAt(runnerIndex);

                    runner.baseIndex = targetBaseIndex;

                    runner.isRunning = true;
                    float roadWidth = 10.0f;
                    Vector3 p0 = roads[roadIndex].Find("p0").transform.position;
                    Vector3 p1 = roads[roadIndex].Find("p1").transform.position;
                    runner.waypoints.Clear();
                    if(Vector3.Distance(p0, runner.transform.position) > Vector3.Distance(p1, runner.transform.position)) {
                        Vector3 temp = p0;
                        p0 = p1;
                        p1 = temp;

                    }

                    float baseRadius = 10.0f;
                    Vector3 roadDir = Vector3.Normalize((p1 - p0));
                    Vector3 roadSide = Vector3.Cross(roadDir, Vector3.up);
                    p0 += roadSide * Random.Range(-0.5f, 0.5f) * roadWidth;
                    p1 += roadSide * Random.Range(-0.5f, 0.5f) * roadWidth;
                    runner.waypoints.Add(p0);
                    runner.waypoints.Add(p1);
                    //float randomAngle = Random.Range()
                    Vector2 randomOffset = Random.insideUnitCircle * baseRadius;
                    runner.waypoints.Add(bases[targetBaseIndex].position + new Vector3(randomOffset.x, 0, randomOffset.y));

                    runner.waypointIndex = 0;
                    runner.targetPosition = runner.waypoints[runner.waypointIndex];

                    runner.anim.CrossFade("Armature|run");
                    runner.transform.Translate(new Vector3(0, 0, 30 * Time.deltaTime));
                }
            }
        }

        runnersAreRunning = true;
        /*for(int ri = 0; ri < runners.Count; ri++) {
            Runner runner = runners[ri];
            int targetBaseIndex = Random.Range(0, bases.Length);
            if(targetBaseIndex == runner.baseIndex) continue;


        }*/
    }

    public int[][] CalculateRunnerShifting(float[] attractionRatios) {
        List<int[]> shiftCount = new();
        for(int i = 0; i < runnerCountAtBase.Length; i++) {
            int runnerCount = runnerCountAtBase[i];
            shiftCount.Add(
                new int[2] {
                    Mathf.RoundToInt(runnerCount * attractionRatios[(i + 1) % BASE_COUNT]),
                    Mathf.RoundToInt(runnerCount * attractionRatios[(i + 2) % BASE_COUNT])
                }
            );
        }
        return shiftCount.ToArray();
    }

    // Update is called once per frame
    void Update() {
        if(runnersAreRunning) {
            int remainingRunners = 0;
            for(int ri = 0; ri < runners.Count; ++ri) {
                Runner runner = runners[ri];
                if(runner.isRunning) {
                    remainingRunners++;
                    Vector3 dp = runner.targetPosition - runner.transform.position;
                    float dist = dp.magnitude;
                    Vector3 dir = dp / dist;
                    float targetRotation = Mathf.Atan2(dir.x, dir.z);
                    float rotation = Mathf.MoveTowardsAngle(runner.transform.eulerAngles.y, targetRotation * Mathf.Rad2Deg, Time.deltaTime * 360.0f * 2.0f);
                    runner.transform.eulerAngles = new Vector3(0, rotation, 0);
                    float moveAmount = runner.runSpeed * Time.deltaTime;
                    if(dist <= moveAmount) {
                        runner.waypointIndex++;
                        if(runner.waypointIndex < runner.waypoints.Count) {
                            runner.targetPosition = runner.waypoints[runner.waypointIndex];
                        }
                        else {
                            runner.isRunning = false;
                            runner.transform.position = runner.targetPosition;
                            runner.anim.CrossFade("Armature|idle");

                            Debug.Assert(Vector3.Distance(runner.transform.position, bases[runner.baseIndex].position) < 15.0f);
                        }
                    }
                    else {
                        runner.transform.position = runner.transform.position + dir * moveAmount;
                    }
                }
            }
            if(remainingRunners == 0) {
                Debug.Log("Runners at bases: " + runnerCountAtBase[0] + ", " + runnerCountAtBase[1] + ", " + runnerCountAtBase[2]);
                runnersAreRunning = false;
            }
        }
    }
}
