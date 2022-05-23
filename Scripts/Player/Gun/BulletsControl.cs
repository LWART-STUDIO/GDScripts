using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletsControl : MonoBehaviour
{
    [SerializeField] private GunsDataSo _gunsData;
    [SerializeField] private GameObject _bulletsHolder;
    [SerializeField] private Sprite[] _bulletImage=new Sprite[2] {null,null};
    [SerializeField] private GameObject _bulletDisplayPrefub;
    [SerializeField] private TMP_Text _bulletsCountText;
    [SerializeField] private GameObject[] _bullets;
    [SerializeField] private GameObject _panel;
    private PlayerAnimator _animator;
    private int _bulletsCount;
    private int _bulletsMaxCount;
    [SerializeField]private bool _canShoot;
    private bool _doReserch=true;

    public bool Visibale;
    public bool CanShoot => _canShoot;
    private PlayerConector _conector;
    private bool _corutineEnded = false;


    private void Start()
    {
        _conector=FindObjectOfType<PlayerConector>();
        _animator = FindObjectOfType<PlayerAnimator>();
        _canShoot = true;
        _bullets = new GameObject[30] {null,null,null,null,null,
                                       null,null,null,null,null,
                                       null,null,null,null,null,
                                       null,null,null,null,null,
                                       null,null,null,null,null,
                                       null,null,null,null,null,};
        

    }
    public void Shoot()
    {
        _bulletsCount--;
        _bulletsCountText.text = "" + _bulletsCount;
        for (int i=0; i < _bulletsMaxCount; i++)
        {
            if( _bullets[i] != null && _bullets[i].GetComponent<Image>().sprite == _bulletImage[0])
            {
                _bullets[i].GetComponent<Image>().sprite = _bulletImage[1];
                break;
            }
        }
    }
    private void Update()
    {
        if (_doReserch)
        {
            
            if (_conector.GunIndex != -1)
            {
                _bulletsMaxCount = _gunsData.GunsData[_conector.GunIndex].GunData.Gun.NumberOfBullets;
                _bulletsCount = _bulletsMaxCount;
                _bulletsCountText.text = "" + _bulletsCount;
                Debug.Log(_conector.GunIndex);
                for (int i = 0; i < _bulletsMaxCount; i++)
                {
                    GameObject bulletDisplay = Instantiate(_bulletDisplayPrefub, _bulletsHolder.transform);
                    bulletDisplay.GetComponent<Image>().sprite = _bulletImage[0];
                    _bullets[i] = bulletDisplay;
                }
                _doReserch = false;
            }
            
        }
        if (_bulletsCount <= 0)
        {
            _bulletsCount = 0;
            _bulletsCountText.text = "" + _bulletsCount;
            if (_animator.Reloded)
            {
                StartCoroutine(ReloudPause());
            }
        }
        if (_bulletsCount > _bulletsMaxCount)
        {
            _bulletsCount = _bulletsMaxCount;
            _bulletsCountText.text = "" + _bulletsCount;
        }
        if (Visibale)
        {
            _panel.SetActive(true);
        }
        else { _panel.SetActive(false); }
        
        if (!_animator.Reloded)
        {
            _canShoot = false;
        }
        else if(_corutineEnded)
        {
            _canShoot = true;
            _corutineEnded = false;
        }
        
    }
    public void Reloud()
    {
        if (_animator.Reloded)
        {
            if (_bulletsCount < _bulletsMaxCount)
            {
                if (_canShoot)
                {
                    StartCoroutine(RelodBullet());
                    _animator.Reloud = true;
                    _canShoot = false;
                }

                

            }
        }

    }
   
    IEnumerator RelodBullet()
    {
        for (int i = 0; i < _bulletsMaxCount; i++)
        {
            if (_bulletsCount < _bulletsMaxCount)
            {
                _bulletsCount++;
                _bulletsCountText.text = "" + _bulletsCount;
            }
            _bullets[_bulletsMaxCount-i-1].GetComponent<Image>().sprite = _bulletImage[0];
            yield return new WaitForSecondsRealtime(0.2f);
        }
        

        StopCoroutine(RelodBullet());
        _canShoot = true;
        _corutineEnded = true;

    }
    private IEnumerator ReloudPause()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Reloud();
    }
}
