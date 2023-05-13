using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Material material = GetComponent<MeshRenderer>().materials[0];
        material.mainTextureOffset = material.mainTextureOffset + speed * Time.deltaTime;
    }
}
