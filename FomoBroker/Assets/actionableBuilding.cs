using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class actionableBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("this is clicked building");
    }

    void OnMouseEnter()
    {
       gameObject.transform.GetChild(0).gameObject.SetActive(true); //shows clickHint on hover
        Debug.Log("this is hovered over building");
    }
}
