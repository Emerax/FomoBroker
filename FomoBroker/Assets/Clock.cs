using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform arm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(float time, float oneRoundTime) {
        if(time < 0.0f) {
            time = 0.0f;
        }
        time = (float)(int)time;
        arm.localEulerAngles = new Vector3(-90 - (time/oneRoundTime) * 360.0f, -90, 90);
    }
}
