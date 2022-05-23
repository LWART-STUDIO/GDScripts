using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class ExploidOnTrigger : MonoBehaviour
{
    private Explosion _explosion;
    private void Start()
    {
        _explosion = GetComponent<Explosion>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BulletMover>(out BulletMover bullet))
        {
            _explosion.Exploid();
            Destroy(gameObject);
        }
    }
}
