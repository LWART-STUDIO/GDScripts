using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HontageBody : MonoBehaviour
{
    [SerializeField] private Hontage _hontage;

    private void Start()
    {
        _hontage=GetComponentInParent<Hontage>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<BulletMover>(out BulletMover bulletMover))
        {
            _hontage.GetHit();
        }
    }
}
