using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundState : ActivityState {
    
    public override void Init ()
    {
        UiController.Instance.globalStatusText.text = "Target Found!";
        UiController.Instance.foundWindow.SetActive(true);
        PlacementController.Instance.EnableProximiterVisibility(true);
    }

    public override bool SetNextState (ActivityState inputNextState)
    {
        return (inputNextState == ActivityStateManager.Instance.TargetDefinitionState);
    }

    public override void Tick ()
    {
        
    }

    public override void Cleanup ()
    {
        UiController.Instance.foundWindow.SetActive(false);
    }
}
