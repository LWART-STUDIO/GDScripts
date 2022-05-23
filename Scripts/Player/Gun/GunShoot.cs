using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private GunSO _gunData;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private bool _multiShot;
    [SerializeField] private int _nuberOfBullets;
    [SerializeField] private float _projectileSpread;
    [SerializeField] private GameObject _currentGun;
    [SerializeField]private SkinnedMeshRenderer[] _meshRenderers;
    [SerializeField] private MeshRenderer[] _meshRenderer;
    [SerializeField] private WeaponContorl[] _weaponContorl;
    [SerializeField] private bool _avtomat;
    [SerializeField] private ParticleSystem _flash;
    private int _countOfBullets=0;
    private Animator _animator=null;
    [SerializeField] private AudioSource[] _audio;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private bool _isSkinMesh;



    public bool IsHoldingGranade;
    private void Start()
    {
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        _weaponContorl = FindObjectsOfType<WeaponContorl>();

        if (gameObject.GetComponentInChildren<Animator>())
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
        }
        if (_currentGun.GetComponentsInChildren<SkinnedMeshRenderer>()!=null)
        {
            _meshRenderers = _currentGun.GetComponentsInChildren<SkinnedMeshRenderer>();
        }
        if (_currentGun.GetComponentsInChildren<MeshRenderer>()!=null)
        {
            _meshRenderer = _currentGun.GetComponentsInChildren<MeshRenderer>();
        }

      //  _nuberOfBullets = _gunData.Gun.NumberOfBullets;      
    }

    public void Shoot(Vector3 pointToShoot)
    {
        if (!IsHoldingGranade)
        {
            if(_animator != null)
            {
                _animator.Play("Shoot"); 
            }

            _flash.Play();

            var BULETDATA = _gunData.Gun.BulletsData.bullets[Random.Range(0, _gunData.Gun.BulletsData.bullets.Length)];
            if (!_multiShot && !_avtomat)
            {
                _audio[Random.Range(0, _audio.Length)].Play();
                float x = Random.Range(-_projectileSpread, _projectileSpread);
                float y = Random.Range(-_projectileSpread, _projectileSpread);
                GameObject bullet = Instantiate(BULETDATA.BulletPrefub, _shootPoint.position, _shootPoint.rotation);
                bullet.transform.LookAt(pointToShoot);
                BulletMover bulletMover = bullet.GetComponent<BulletMover>();
                bulletMover.PointToMove = pointToShoot+new Vector3(x, y, 0);
                bulletMover.Speed = BULETDATA.BulletSpeed;
                Debug.Log("Shoot 1");
            }
            else if (_multiShot&&!_avtomat)
            {

                for (int i = 0; i < _nuberOfBullets; i++)
                {
                    _audio[Random.Range(0, _audio.Length)].Play();
                    float x = Random.Range(-_projectileSpread, _projectileSpread);
                    float y = Random.Range(-_projectileSpread, _projectileSpread);
                    GameObject bullet = Instantiate(BULETDATA.BulletPrefub, _shootPoint.position, _shootPoint.rotation);
                    bullet.transform.LookAt(pointToShoot);
                    BulletMover bulletMover = bullet.GetComponent<BulletMover>();
                    bulletMover.PointToMove = pointToShoot + new Vector3(x, y, 0);
                    bulletMover.Speed = BULETDATA.BulletSpeed;
                    Debug.Log("Shoot 2");
                }
            }
            else if (_avtomat)
            {
                _audio[Random.Range(0, _audio.Length)].Play();
                StartCoroutine(AvtomatShoot(pointToShoot));
            }
        }
        else if (IsHoldingGranade)
        {
            _playerAnimator.ThrowGranade = true;
            
            for (int i = 0; i < _weaponContorl.Length; i++)
            {
                _weaponContorl[i].ThrowGaranade(pointToShoot);

                if(_weaponContorl[i].GetComponentInChildren<GunShoot>() != null)
                {
                    StartCoroutine(_weaponContorl[i].GetComponentInChildren<GunShoot>().GranadeTimer(pointToShoot));
                }
            }

        }

    }
    public void EqupGranade()
    {
        IsHoldingGranade=true;
        if (_meshRenderers!= null)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].enabled = false;
            }
        }
        if(_meshRenderer != null)
        {
            for (int i = 0; i < _meshRenderer.Length; i++)
            {
                _meshRenderer[i].enabled = false;
            }
        }
    }
    IEnumerator AvtomatShoot(Vector3 pointToShoot)
    {
        for(int i=0; i<_nuberOfBullets; i++)
        {
            var BULETDATA = _gunData.Gun.BulletsData.bullets[Random.Range(0, _gunData.Gun.BulletsData.bullets.Length)];
            float x = Random.Range(-_projectileSpread, _projectileSpread);
            float y = Random.Range(-_projectileSpread, _projectileSpread);
            GameObject bullet = Instantiate(BULETDATA.BulletPrefub, _shootPoint.position, _shootPoint.rotation);
            bullet.transform.LookAt(pointToShoot);
            BulletMover bulletMover = bullet.GetComponent<BulletMover>();
            bulletMover.PointToMove = pointToShoot+ new Vector3(x, y, 0);
            bulletMover.Speed = BULETDATA.BulletSpeed;
            Debug.Log("Shoot 3");
            yield return new WaitForSecondsRealtime(0.1f);
        } 
    }
   public IEnumerator GranadeTimer(Vector3 pointToShoot)
    {
        yield return new WaitForSecondsRealtime(0.4f);

        StartCoroutine(WeaponShow());
        
        StopCoroutine(GranadeTimer(pointToShoot));
    }
    IEnumerator WeaponShow()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (_meshRenderers != null)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].enabled = true;
            }
        }
        if (_meshRenderer != null)
        {
            for (int i = 0; i < _meshRenderer.Length; i++)
            {
                _meshRenderer[i].enabled = true;
            }
        }
        IsHoldingGranade = false;

        StopCoroutine(WeaponShow());
    }
}
