using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private bool _wantShoot;
    public bool WantShoot => _wantShoot;

    private Vector3 _mousePosition;
    public Vector3 TapPosition => _mousePosition;
    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            _mousePosition = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _wantShoot = true;
        }
        else
        {
            _wantShoot = false;
        }
    }
}

