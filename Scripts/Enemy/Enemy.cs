using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _isAlive = true;
    public bool IsAlive => _isAlive;
    [EditorButton(nameof(Kill), "Kill", activityType: ButtonActivityType.OnPlayMode)]
    [SerializeField] private Rigidbody[] _ragdollRigidBodys;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Animator _animator;
    private EnemyAnimationController _controller;
    [SerializeField] private GameObject _warningMessege;
    private GameObject _player;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GunSO _gunData;
    [SerializeField] private AudioSource[] _audio;
    private EnemyHealth _enemyHealth;
    [SerializeField] private SkinnedMeshRenderer[] _darkMaterials;
    [SerializeField] private SkinnedMeshRenderer[] _lightMaterials;
    [SerializeField] private Material _dethLightMaterial;
    [SerializeField] private Material _dethDarkMaterial;
    [SerializeField] private GameObject _gun;
    public bool KilldInHead = false;
    [SerializeField] private bool _useRocket;


    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _player = FindObjectOfType<PlayerConector>().gameObject;
        _animator = GetComponent<Animator>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollRigidBodys = rigidbodies;

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            _ragdollRigidBodys[i] = rigidbodies[i];
            _ragdollRigidBodys[i].isKinematic = true;
        }
        _controller = GetComponent<EnemyAnimationController>();

    }
    public void Shoot()
    {
        if (_isAlive)
        {

            if (!_useRocket)
            {
                var BULETDATA = _gunData.Gun.BulletsData.bullets[Random.Range(0, _gunData.Gun.BulletsData.bullets.Length)];
                _audio[Random.Range(0, _audio.Length)].Play();
                GameObject bullet = Instantiate(BULETDATA.BulletPrefub, _shootPoint.position, _shootPoint.rotation);
                bullet.transform.LookAt(_player.transform.position + new Vector3(0.1f, 0.3f, 0));
                BulletMover bulletMover = bullet.GetComponent<BulletMover>();
                bulletMover.PointToMove = _player.transform.position + new Vector3(0.1f, 0.3f, 0);
                bulletMover.Speed = 3f;
                Debug.Log("Shoot 1");
            }
            else if (_useRocket)
            {
                
                RoketLaunh roketLaunh= GetComponent<RoketLaunh>();
                roketLaunh.SetCorrective(3f,0f,new Vector3(0,0,0));
                roketLaunh.Lanch = true;
            }
        }
    }





    public void Kill()
    {
        if (_isAlive)
        {
            _enemyHealth.GetKilled();
            _warningMessege.SetActive(false);
            if (_controller != null)
            {
                _controller.enabled = false;
            }

            if (_gun != null)
            {
                _gun.SetActive(false);
            }

            for (int i = 0; i < _darkMaterials.Length; i++)
            {
                _darkMaterials[i].material = _dethDarkMaterial;
            }
            for (int i = 0; i < _lightMaterials.Length; i++)
            {
                _lightMaterials[i].material = _dethLightMaterial;
            }
            if (_animator != null)
            {
                _animator.enabled = false;

            }

            for (int i = 0; i < _ragdollRigidBodys.Length; i++)
            {
                _ragdollRigidBodys[i].isKinematic = false;
            }

            _isAlive = false;
            _enemyCounter.KilledInHead = KilldInHead;
            _enemyCounter.CountPlus();

            // Destroy(this);
        }

    }
}
