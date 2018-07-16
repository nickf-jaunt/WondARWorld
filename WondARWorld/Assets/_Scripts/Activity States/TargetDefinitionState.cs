using System;
using UnityEngine;

public class TargetDefinitionState : ActivityState
{
    public override void Init ()
    {
        UiController.Instance.targetDefinitionWindow.SetActive(true);
        PlacementController.Instance.EnableProximiterVisibility(true);
    }

    public override bool SetNextState (ActivityState inputNextState)
    {
        return (inputNextState == ActivityStateManager.Instance.LimboState);
    }

    public override void Tick ()
    {
        UiController.Instance.setTargetButton.gameObject.SetActive(PlacementController.Instance.ProximiterPlaced);

        if (ArCoreController.Instance.ArPlanesDetected)
        {
            UiController.Instance.globalStatusText.text = "A surface was detected!";
        }
        else
        {
            UiController.Instance.globalStatusText.text = "Trying to detect a surface...";
        }
    }

    public override void Cleanup()
    {
        UiController.Instance.targetDefinitionWindow.SetActive(false);
    }
}
