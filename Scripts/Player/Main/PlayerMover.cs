using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMover : MonoBehaviour
{
    private PathCreator _path;
    private float _speed;
    public float Speed => _speed;
    private EndOfPathInstruction _end;
    private Quaternion _rotation;
    public Quaternion Rotation => _rotation;
    private float _distanceGone;
    private Vector3 _position;
    public Vector3 Position => _position;
    [HideInInspector] public bool DoRoation = true;
    private void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
    }
    private void FixedUpdate()
    {
        _distanceGone += _speed * Time.fixedDeltaTime;
        _position = _path.path.GetPointAtDistance(_distanceGone,_end);
        transform.position = _position;
        _rotation = _path.path.GetRotationAtDistance(_distanceGone, _end);

        if ( DoRoation)
        {
            transform.rotation = _rotation;
        }
        
    }
    public void SetPath(PathCreator pathCreator)
    {
        _path = pathCreator;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetDoRotation(bool doRoration)
    {
        DoRoation = doRoration;
    }
    public void SetHowWolkOnPath(EndOfPathInstruction endOfPathInstruction)
    {
        _end = endOfPathInstruction;
    }
}
