using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class SetLookAtPlayer : MonoBehaviour
{
    [SerializeField]private bool _handAim=false;
    [SerializeField] private MultiAimConstraint _chestsAimConstraint;
    [SerializeField] private MultiAimConstraint _headAimConstraint;
    [SerializeField] private TwoBoneIKConstraint _rightHandIKConstraint;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _headAimConstraint.data.sourceObjects.Add(new WeightedTransform(_camera.transform,0.577f));
        _chestsAimConstraint.data.sourceObjects.Add(new WeightedTransform(_camera.transform, 1f));
        if (_handAim)
        {
            _rightHandIKConstraint.data.target= _camera.transform;
        }
    }

}
