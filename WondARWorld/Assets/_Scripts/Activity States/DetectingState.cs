using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingState : ActivityState
{
    public override void Init()
    {
        UiController.Instance.detectingWindow.SetActive(true);
    }

    public override bool SetNextState(ActivityState inputNextState)
    {
        return (inputNextState == ActivityStateManager.Instance.ScanningState || inputNextState == ActivityStateManager.Instance.TargetDefinitionState);
    }

    public override void Tick()
    {
        if (ArCoreController.Instance.AnchorLost) {
            ActivityStateManager.Instance.SetNextState(ActivityStateManager.Instance.TargetDefinitionState);
        } else {
            UiController.Instance.globalStatusText.text = PlacementController.Instance.ProximiterDistance.ToString();
            UiController.Instance.scanButton.SetActive(PlacementController.Instance.ProximiterNearby);
        }
    }

    public override void Cleanup()
    {
        UiController.Instance.detectingWindow.SetActive(false);
    }
}
