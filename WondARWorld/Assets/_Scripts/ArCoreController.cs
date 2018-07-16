using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ArCoreController : MonoBehaviour
{
    public static ArCoreController Instance { get; private set; }

    public bool AnchorLost {
        get {
            return _setAnchor.TrackingState == TrackingState.Stopped;
        }
    }

    private List<DetectedPlane> _arPlanes;

    private Anchor _setAnchor;

    public bool ArPlanesDetected
    {
        get
        {
            Session.GetTrackables<DetectedPlane>(_arPlanes);
            return _arPlanes.Count > 0;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        _arPlanes = new List<DetectedPlane>();
    }

    public bool RaycastToPlane(Vector3 inputCameraPos, Vector3 inputTouchPos, out Transform outputAnchorTrans)
    {
        TrackableHit hit;
        outputAnchorTrans = null;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(inputTouchPos.x, inputTouchPos.y, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(inputCameraPos - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                _setAnchor = hit.Trackable.CreateAnchor(hit.Pose);

                if (_setAnchor.TrackingState == TrackingState.Tracking) {
                    outputAnchorTrans = _setAnchor.transform;
                }

                return true;
            }
        }

        return false;
    }
}
