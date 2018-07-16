using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateManager : MonoBehaviour
{
    public static ActivityStateManager Instance { get; private set; }

    public ActivityState CurrentState { get; private set; }
    public TargetDefinitionState TargetDefinitionState { get; private set; }
    public LimboState LimboState { get; private set; }
    public DetectingState DetectingState { get; private set; }
	public ScanningState ScanningState { get; private set; }
    public FoundState FoundState { get; private set; }

    public void SetState (string stateName)
    {
        if( stateName == TargetDefinitionState.ToString()) {
            SetNextState(TargetDefinitionState);
        }
        else if( stateName == LimboState.ToString()) {
            SetNextState(LimboState);
        }
    }
    
    public void SetStateToLimbo ()
    {
        SetNextState(LimboState);
    }

    public void SetStateToDetecting ()
    {
        SetNextState(DetectingState);
    }
    
	public void SetStateToScanning()
	{
		SetNextState (ScanningState);
	}
    
    public void SetStateToTargetDefinitionState ()
    {
        SetNextState(TargetDefinitionState);
    }

    public void SetNextState (ActivityState inputNextState)
    {
        if (CurrentState != null) {
            if (CurrentState.SetNextState(inputNextState)) {
                CurrentState.Cleanup();
                CurrentState = inputNextState;
                inputNextState.Init();
            }
        }
        else {
            CurrentState = inputNextState;
            inputNextState.Init();
        }
    }

    private void Awake ()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start ()
    {
        TargetDefinitionState = new TargetDefinitionState();
        LimboState = new LimboState();
        DetectingState = new DetectingState();
		ScanningState = new ScanningState ();
        FoundState = new FoundState();
        SetNextState(TargetDefinitionState);
    }

    private void Update ()
    {
        if (CurrentState != null)
            CurrentState.Tick();
    }
}
