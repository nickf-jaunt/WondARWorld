using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximiter : MonoBehaviour
{
    [SerializeField]
    private Transform _selfTrans;
    [SerializeField]
    private BoxCollider _bodyCollider;
    [SerializeField]
    private Renderer[] _bodyRenderers;
    [SerializeField]
    private ParticleSystem _lineParticules;
    [SerializeField]
    private float _maxDistance;

    private Transform _toTrans;
    private float _initialToDistance;

    private const float MAX_DIST = 3.0f;

    public void Init (Transform inputAnchorTrans, Transform inputToTrans)
    {
        _toTrans = inputToTrans;
        _selfTrans.parent = inputAnchorTrans;
        _selfTrans.localPosition = new Vector3(0, _bodyCollider.bounds.size.y / 2, 0);
        //_initialToDistance = Vector3.Distance(_selfTrans.position, inputToTrans.position);
        _initialToDistance = MAX_DIST;
    }

    public void EnableVisiblility(bool inputVisible)
    {
        if (inputVisible)
        {
            for (int i = 0; i < _bodyRenderers.Length; i++)
                _bodyRenderers[i].enabled = true;

            if (!_lineParticules.isEmitting) {
                _lineParticules.Clear();
                _lineParticules.Play();
            };
        }
        else
        {
            for (int i = 0; i < _bodyRenderers.Length; i++)
                _bodyRenderers[i].enabled = false;
            if(_lineParticules.isEmitting) {
                _lineParticules.Stop();
                _lineParticules.Clear();
            }
        }
    }

	public bool InFieldOfView(Camera inputCamera)
	{
		Vector3 screenPoint = inputCamera.WorldToViewportPoint(_selfTrans.position);
		return (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1);
	}

    public float CalculateDistance ()
    {
        return Vector3.Distance(_selfTrans.position, _toTrans.position);
    }

    public float CalculateFactorDistance ()
    {
        return CalculateDistance() / _maxDistance;
    }

    public bool IsNearToTarget ()
    {
        return CalculateDistance() <= _initialToDistance;
    }
}
