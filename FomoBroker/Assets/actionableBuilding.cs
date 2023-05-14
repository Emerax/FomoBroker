using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.EventSystems;

public class actionableBuilding : MonoBehaviour
{
    public Action<ActionType,int> ActionEvent;
    [SerializeField]
    private ActionType action;
    


   


    //determine this building
   
     [SerializeField]
    private int religionBuilding = 1; //from1 1 left to right 
    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("this is clicked building");
        ActionEvent.Invoke(action,religionBuilding);
    }

    void OnMouseEnter()
    {
       gameObject.transform.GetChild(0).gameObject.SetActive(true); //shows clickHint on hover
        Debug.Log("this is hovered over building");
    }
}
