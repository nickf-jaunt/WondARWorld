using System;

public class LimboState : ActivityState
{
    public override void Init ()
    {
        UiController.Instance.globalStatusText.text = "Proximiter is successfully set";
        UiController.Instance.limboDefinitionWindow.gameObject.SetActive(true);
        PlacementController.Instance.EnableProximiterVisibility(false);
    }

    public override bool SetNextState (ActivityState inputNextState)
    {
        return (inputNextState == ActivityStateManager.Instance.DetectingState || inputNextState == ActivityStateManager.Instance.TargetDefinitionState);
    }

    public override void Tick ()
    {
        
    }

    public override void Cleanup()
    {
        UiController.Instance.limboDefinitionWindow.gameObject.SetActive(false);
    }
}
