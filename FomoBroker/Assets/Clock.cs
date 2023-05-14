using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform arm;
    bool buzzing = false;
    bool hasCompleted = false;
    float buzzTimer;
    Vector3 restPosition;


    // Start is called before the first frame update
    void Start()
    {   
        restPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(buzzing) {
            buzzTimer -= Time.deltaTime;
            if(buzzTimer < 0.0f) {
                buzzing = false;
                transform.position = restPosition;
            }
            else {
                transform.position = restPosition + Random.onUnitSphere * 2.0f;
            }
        }
    }

    public void SetTime(float time, float oneRoundTime) {
        if(time < 0.0f) {
            time = 0.0f;
            if(!hasCompleted) {
                buzzing = true;
                buzzTimer = 2.0f;
                hasCompleted = true;
            }
        }
        else {
            hasCompleted = false;
            buzzing = false;
        }
        time = (float)(int)time;
        arm.localEulerAngles = new Vector3(-90 - (time/oneRoundTime) * 360.0f, -90, 90);
    }
}
