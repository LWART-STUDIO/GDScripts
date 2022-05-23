using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Радиус взрыва")]
    [SerializeField] private float _radius;
    [Header("Сила взрыва")]
    [SerializeField] private float _power;
    [Header("Звук взрыва")]
    [SerializeField] private AudioSource[] _audio;
    [Header("Партиклы взрыва")]
    [SerializeField] private ParticleSystem _particleSystem;
    

    public void Exploid()
    {
        Vector3 explosionPosition=transform.position;
        Collider[] colliders=Physics.OverlapSphere(explosionPosition, _radius);
        Debug.Log("Explosion");
        _particleSystem.Play();
        _audio[Random.Range(0, _audio.Length)].Play(); 
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Kill();
            }
            if(rb != null)
            {
                rb.AddExplosionForce(_power,explosionPosition,_radius);
            }
            if(hit.TryGetComponent<Barrel>(out Barrel barrel)){
                barrel.DoExploid();
            }
            if (hit.TryGetComponent<DestroyOnBullet>(out DestroyOnBullet meshDestroy))
            {
                meshDestroy.Destroy = true;
            }
        }
    }
}
