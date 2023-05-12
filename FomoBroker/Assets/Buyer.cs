using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        animation.Play("Armature|idle");
        animation["Armature|run"].speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) {
            animation.Play("Armature|run");
            transform.Translate(new Vector3(0, 0, 30*Time.deltaTime));
        }
        else {
            animation.Play("Armature|idle");
        }
    }
}
