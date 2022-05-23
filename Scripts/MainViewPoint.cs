using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MainViewPoint : MonoBehaviour
{
    [SerializeField] private PathCreator _path;
    public float Speed=1.2f;
    [SerializeField] private EndOfPathInstruction _end;
    private float _distanceGone;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _speedToRotation;
    public bool DoRotation;
    
    

    private void FixedUpdate()
    {
        _distanceGone += Speed * Time.fixedDeltaTime;
        if(_path != null)
        {
            transform.position = _path.path.GetPointAtDistance(_distanceGone, _end);
        }
        

        if (DoRotation)
        {
            Quaternion OriginalRot = _player.transform.localRotation;
            _player.transform.LookAt(gameObject.transform.position);
            Quaternion NewRot = _player.transform.localRotation;
            _player.transform.localRotation = OriginalRot;
            _player.transform.localRotation = Quaternion.Lerp(_player.transform.localRotation, NewRot, _speedToRotation * Time.fixedUnscaledDeltaTime);
        }
    }
}
