using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Vector3 PointToMove;
    public float Speed;
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private AudioSource[] _meatHit;
    [SerializeField] private bool _isExplosion;
    [SerializeField] private Explosion _explosion;
    private Collider _collider;
    [SerializeField] private bool _isEnemy;
    [SerializeField] private GameObject _model;
    private GameObject _deadPanel;
    public bool HitEnemy=false;
    [SerializeField] private bool _pistolBullet;
    public bool PistolBullet=>_pistolBullet;
    [SerializeField] private bool _riffleBullet;
    public bool RiffleBullet=>_riffleBullet;
    [SerializeField] private bool _shotgunBullet;
    public bool ShotgunBullet=>_shotgunBullet;
    private bool _resized = false;
    private void Awake()
    {
        _meatHit = FindObjectOfType<MeatSond>().MeatSounds;
        _explosion=GetComponent<Explosion>();
        _collider = GetComponent<Collider>();
        _deadPanel = FindObjectOfType<DeadPanelMarker>().gameObject;
        
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointToMove, Speed * Time.fixedUnscaledDeltaTime);
            if (_isEnemy)
            {
                if (_resized == false)
                {
                    StartCoroutine(EnemyBulletColiderResizer());
                }
            }
            if (transform.position == PointToMove)
            {
                Destroy();
            }
        }
        
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointToMove, Speed * Time.unscaledDeltaTime);
            if (_isEnemy)
            {
                if (_resized == false)
                {
                    StartCoroutine(EnemyBulletColiderResizer());
                }
                
            }
            if (transform.position == PointToMove)
            {
                Destroy();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!_isEnemy)
        {
            if (collision.gameObject.TryGetComponent<EnemyBody>(out EnemyBody enemyBody))
            {

                ContactPoint point = collision.contacts[0];
                Instantiate(_bloodEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                _meatHit[Random.Range(0, _meatHit.Length)].Play();
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }
                Destroy();
            }
            else if (collision.gameObject.TryGetComponent<EnemyHead>(out EnemyHead enemyHead))
            {

                ContactPoint point = collision.contacts[0];
                Instantiate(_bloodEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                _meatHit[Random.Range(0, _meatHit.Length)].Play();
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }
                Destroy();
            }
            else if (!collision.gameObject.TryGetComponent<PlayerConector>(out PlayerConector playerConector) && collision.gameObject.layer!=2)
            {
                Debug.Log(collision.gameObject.name);
                ContactPoint point = collision.contacts[0];
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }
                if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
                {
                    Debug.Log("Wall");
                    Instantiate(_hitEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                }
                Destroy();
            }
            else if (collision.gameObject.TryGetComponent<Hontage>(out Hontage hontage1))
            {

                ContactPoint point = collision.contacts[0];
                Instantiate(_bloodEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                _meatHit[Random.Range(0, _meatHit.Length)].Play();
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }
                Destroy();
            }

        }
        else
        {
            if (collision.gameObject.TryGetComponent<PlayerConector>(out PlayerConector playerConector))
            {
                ContactPoint point = collision.contacts[0];
                Instantiate(_bloodEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                _meatHit[Random.Range(0, _meatHit.Length)].Play();
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }

                ProjectTools.SetChildrenActive(_deadPanel, true);
                Destroy();
            }
            if (collision.gameObject.TryGetComponent<BulletMover>(out BulletMover bulletMover))
            {
               // ContactPoint point = collision.contacts[0];
               // Instantiate(_bloodEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
               // _meatHit[Random.Range(0, _meatHit.Length)].Play();
                _collider.enabled = false;
                if (_isExplosion)
                {
                    _explosion.Exploid();
                }

                
                Destroy();
            }
            /*            else if (!collision.gameObject.TryGetComponent<EnemyHead>(out EnemyHead Head) && !collision.gameObject.TryGetComponent<EnemyBody>(out EnemyBody Body) && !collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy) && !collision.gameObject.TryGetComponent<Hontage>(out Hontage hontage))
                        {
                            ContactPoint point = collision.contacts[0];
                            _collider.enabled = false;
                            if (_isExplosion)
                            {
                                _explosion.Exploid();
                            }
                            if(collision.gameObject.TryGetComponent<Wall>(out Wall wall))
                            {
                                Debug.Log("Wall");
                                Instantiate(_hitEffect, this.transform.position, Quaternion.LookRotation(-point.normal));
                            }

                            Destroy();
                        }*/
        }

    }
    private void Destroy()
    {
        if (_isExplosion)
        {
            StartCoroutine(TimerToDestroy());
            _model.SetActive(false);
        }
        else
        {
            if (_model != null)
            {
                _model.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
    IEnumerator TimerToDestroy()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
    private IEnumerator EnemyBulletColiderResizer()
    {
        _resized = true;
        yield return new WaitForSecondsRealtime(4f);
        {
            SphereCollider collider= GetComponent<SphereCollider>();
            float radius = collider.radius;
            collider.radius = radius*5f;
            
        }
    }
}
