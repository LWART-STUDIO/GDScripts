using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadracopterBody : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;
    private Collider _collider;
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _skinnedMeshRenderers=GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent<BulletMover>(out BulletMover bulletMover))
        {
            _collider.enabled = false;
            _audioSource.Play();
            _particleSystem.Play();
            for(int i=0; i<_skinnedMeshRenderers.Length; i++)
            {
                _skinnedMeshRenderers[i].enabled = false;
            }
            _enemyHealth.GetKilled();
        }
    }

}
