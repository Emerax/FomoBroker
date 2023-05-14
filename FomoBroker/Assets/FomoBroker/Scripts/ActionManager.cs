using System.Collections.Generic;
using System.Linq;
using System;

public class ActionManager {

    public Action<ActionType,int> ActionEvent;

     private readonly List<actionableBuilding> buildings;

    public ActionManager(List<actionableBuilding> buildings) {
        this.buildings = buildings;

        foreach (actionableBuilding b in buildings){
            b.ActionEvent += OnEvent;
        }

        
    }

    
    private void OnEvent(ActionType action, int target){
        
        ActionEvent.Invoke(action,target);
    }
   
}
