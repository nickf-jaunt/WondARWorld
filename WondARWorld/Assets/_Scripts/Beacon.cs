using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    [SerializeField]
    private Animator _beaconAnimator;
    
    private Proximiter _proximiter;

    public void SetProximiter (Proximiter inputProximiter)
    {
        _proximiter = inputProximiter;
    }

	private void Update ()
    {
        float speedFactor = _proximiter.CalculateFactorDistance();
        if (speedFactor > 0) {
            _beaconAnimator.speed = Mathf.Clamp(1 / speedFactor, 0.25f, 3f);
        } else {
            _beaconAnimator.speed = 3f;
        }
        
    }
}
