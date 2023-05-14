using System;
using UnityEngine;
//using UnityEngine.EventSystems;

public class actionableBuilding : MonoBehaviour {
    public Action<ActionType, int> ActionEvent;

    [SerializeField]
    private ActionType action;
    [SerializeField]
    private int religionBuilding = 1; //from1 1 left to right 
    //[SerializeField]
    //private GameObject clickHint;
    // Start is called before the first frame update
    void OnMouseDown() {
        Debug.Log("this is clicked building");
        ActionEvent.Invoke(action, religionBuilding);
    }

    //void OnMouseEnter() {
    //    clickHint.gameObject.SetActive(true);
    //}

    //void OnMouseExit() {
    //    clickHint.gameObject.SetActive(false);
    //}
}
