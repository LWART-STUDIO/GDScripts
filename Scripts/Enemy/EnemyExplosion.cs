using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    private GameObject _deadPanel;
    private Collider _collider;
    private void Awake()
    {
        _deadPanel = FindObjectOfType<DeadPanelMarker>().gameObject;
        _collider=GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<BulletMover>(out BulletMover bulletMover))
        {
            _explosion.Exploid();
            _collider.enabled = false;
        }
        if(collision.TryGetComponent<PlayerConector>(out PlayerConector conector))
        {
            ProjectTools.SetChildrenActive(_deadPanel, true);
            _explosion.Exploid();
            _collider.enabled = false;
        }
    }
   
}
