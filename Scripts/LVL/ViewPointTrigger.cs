using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointTrigger : MonoBehaviour
{
    private MainViewPoint _mainViewPoint;
    private GameObject _player;
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private float _speed;
    private bool _doRotation=false;

    private void Start()
    {
        _mainViewPoint=FindObjectOfType<MainViewPoint>();
        _player=FindObjectOfType<PlayerConector>().gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _mainViewPoint.DoRotation = false;
            _doRotation=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _mainViewPoint.DoRotation = true;
            _doRotation = false;
        }
    }
    private void FixedUpdate()
    {
        if (_doRotation)
        {
            Quaternion OriginalRot = _player.transform.localRotation;
            _player.transform.LookAt(_viewPoint.transform.position);
            Quaternion NewRot = _player.transform.localRotation;
            _player.transform.localRotation = OriginalRot;
            _player.transform.localRotation = Quaternion.Lerp(_player.transform.localRotation, NewRot, _speed * Time.fixedUnscaledDeltaTime);
        }
        
    }
}
