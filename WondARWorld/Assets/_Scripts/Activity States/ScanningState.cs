using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningState : ActivityState {

	private float _timer;
	private const float DURATION = 1.0f;
	private bool _objectDetected;

	public override void Init ()
	{
		_timer = 0.0f;
		_objectDetected = PlacementController.Instance.ProximiterInFieldOfView;
		UiController.Instance.scanningWindow.SetActive (true);
		UiController.Instance.globalStatusText.text = "Scanning ...";
	}

	public override void Tick ()
	{
		if (_timer <= DURATION) {
			_timer += Time.deltaTime;	
		} else {
			if (_objectDetected) {
                Debug.Log("Object was detected");
                ActivityStateManager.Instance.SetNextState(ActivityStateManager.Instance.FoundState);
            } else {
                Debug.Log("Object was not detected");
                ActivityStateManager.Instance.SetNextState(ActivityStateManager.Instance.DetectingState);
			}
		}
	}

	public override bool SetNextState (ActivityState inputNextState)
	{
        return (inputNextState == ActivityStateManager.Instance.FoundState || inputNextState == ActivityStateManager.Instance.DetectingState);
	}

	public override void Cleanup()
	{
		UiController.Instance.scanningWindow.SetActive (false);
	}

}
