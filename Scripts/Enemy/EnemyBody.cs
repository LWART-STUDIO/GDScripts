using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponentInParent<EnemyHealth>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<BulletMover>(out BulletMover bulletMover))
        {
            if (!bulletMover.HitEnemy)
            {
                bool pistol = bulletMover.PistolBullet;
                bool riffle = bulletMover.RiffleBullet;
                bool shotgun = bulletMover.ShotgunBullet;
                bulletMover.HitEnemy = true;
                _health.GetShot(pistol,riffle, shotgun) ;


            }
            
        }
        /*if(_rigidbody.velocity!=new Vector3(0, 0, 0))
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
        }*/
    }
}
