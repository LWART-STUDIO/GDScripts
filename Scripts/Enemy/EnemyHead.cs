using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health=GetComponentInParent<EnemyHealth>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<BulletMover>(out BulletMover bulletMover))
        {
            if (!bulletMover.HitEnemy)
            {

                bulletMover.HitEnemy = true;
                _health.GetHeadShot();
            }
            
        }
        /*if (_rigidbody.velocity != new Vector3(0, 0, 0))
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
        }*/
    }
}
