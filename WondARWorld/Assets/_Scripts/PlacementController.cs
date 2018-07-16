using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    public static PlacementController Instance { get; private set; }

    public bool ProximiterPlaced {
        get {
            return _proximiter != null;
        }
    }

    public bool ProximiterNearby {
        get {
            return _proximiter.IsNearToTarget();
        }
    }

	public bool ProximiterInFieldOfView {
		get 
		{ 
			return _proximiter.InFieldOfView (_mainCam);
		}
	}

    public float ProximiterDistance {
        get {
            return _proximiter.CalculateDistance();
        }
    }
    
    [SerializeField]
    private Proximiter _proximiterPrefab;
    [SerializeField]
    private Beacon _beacon;

	private Camera _mainCam;
    private Transform _mainCamTrans;
    private Proximiter _proximiter;

    private void Awake ()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;

		_mainCam = Camera.main;
        _mainCamTrans = Camera.main.transform;
    }

    public void RaycastAndSpawnProximiter ()
    {
        if (Input.touchCount == 1) 
        {
            Touch touch = Input.GetTouch(0);
            
            Transform anchorTrans;
            if (ArCoreController.Instance.RaycastToPlane(_mainCamTrans.position, touch.position, out anchorTrans))
            {
                if (_proximiter != null)
                {
                    GameObject.Destroy(_proximiter.gameObject);
                }

                Proximiter proximiterInst = GameObject.Instantiate<Proximiter>(_proximiterPrefab);
                proximiterInst.Init(anchorTrans, _mainCamTrans);
                _proximiter = proximiterInst;
                _beacon.SetProximiter(proximiterInst);
            }
        }
    }

    public void EnableProximiterVisibility(bool inputEnable)
    {
        if(_proximiter != null) {
            _proximiter.EnableVisiblility(inputEnable);
        }
    }
}
