using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Animation jumpAnim;
    public Animation legAnim;
    public ParticleSystem downSplashPs;
    public float downSplashTime;

    public ParticleSystem upSplashPs;
    public float upSplashTime;

    float nextJumpTime;
    float currentJumpTimer;

    void resetJumpTime() {
        nextJumpTime = Random.Range(3.0f, 30.0f);
    }
    // Start is called before the first frame update
    void Start()
    {        
        currentJumpTimer = 999.0f;
        resetJumpTime();
        legAnim["Armature|run"].speed = 4.0f;    
    }

    // Update is called once per frame
    void Update()
    {
        nextJumpTime -= Time.deltaTime;
        if(nextJumpTime < 0) {
            resetJumpTime();
            currentJumpTimer = 0.0f;
            jumpAnim.Play();
        }
        if(currentJumpTimer < upSplashTime) {
            currentJumpTimer += Time.deltaTime;
            if(currentJumpTimer >= upSplashTime) {
                upSplashPs.Play();
            }
        }
        else if(currentJumpTimer < downSplashTime) {
            currentJumpTimer += Time.deltaTime;
            if(currentJumpTimer >= downSplashTime) {
                downSplashPs.Play();
            }
        }

    }
}
