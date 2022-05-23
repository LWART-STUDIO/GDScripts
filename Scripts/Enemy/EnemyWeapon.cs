using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private GameObject _camera;
    private void Start()
    {

        _camera = Camera.main.gameObject;
        
    }
    private void FixedUpdate()
    {
        transform.LookAt(_camera.transform);
    }
}
