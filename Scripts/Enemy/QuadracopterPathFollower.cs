using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class QuadracopterPathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _path;
    [SerializeField] private EndOfPathInstruction _end;
    private float _distanceGone;
    [SerializeField] private bool _doRotaton;
    private Vector3 _position;
    private Quaternion _rotation;



    private void FixedUpdate()
    {
        _distanceGone += _speed * Time.fixedDeltaTime;
        transform.position = _path.path.GetPointAtDistance(_distanceGone,_end);
        
        _rotation=_path.path.GetRotationAtDistance(_distanceGone,_end);
        if (_doRotaton)
        {
            transform.rotation=_rotation;
        }
    }
}
