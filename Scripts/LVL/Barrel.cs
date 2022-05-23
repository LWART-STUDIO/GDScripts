using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Explosion))]
[ RequireComponent(typeof(MeshDestroy))]
public class Barrel : MonoBehaviour
{
    private Explosion _explosion;
    private MeshDestroy _meshDestroy;
    private MeshRenderer _renderer;
    private bool _destroed = false;
    private void Start()
    {
       _explosion = GetComponent<Explosion>();
        _meshDestroy = GetComponent<MeshDestroy>();
        _renderer = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_destroed)
        {
            if (collision.collider.TryGetComponent<BulletMover>(out BulletMover bulletMover))
            {
                DoExploid();
                // _explosion.Exploid();
                _destroed = true;
            }
        }
       
    }
    public void DoExploid()
    {
        if (!_destroed)
        {
            _destroed = true;
            _renderer.enabled = false;
            _explosion.Exploid();
            
            // _meshDestroy.DestroyMesh();
        }
    }
}
