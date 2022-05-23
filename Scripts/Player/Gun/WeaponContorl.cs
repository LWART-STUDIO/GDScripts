using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContorl : MonoBehaviour
{
    [SerializeField] private GunShoot[] _gunShoots;
    [SerializeField] private Granade _granade;
    [EditorButton(nameof(GiveGranate), "GiveGranate", activityType: ButtonActivityType.OnPlayMode)]
    [SerializeField] private GameObject _granadePrefab;
    private GameObject _granadeObject;
    private Animator _granadeAnimator;
    private bool _isHoldingGranade;
    public bool IsHoldingGranade => _isHoldingGranade;
    private WeaponContorl[] _weaponContorls;
    private void Update()
    {
        if (_isHoldingGranade)
        {
            _granadeObject.transform.position=transform.position;
            _granadeObject.transform.rotation=transform.rotation;
            
        }
        
            for (int i = 0; i < _weaponContorls.Length; i++)
            {
                if (_weaponContorls[i].GetComponentInChildren<GunShoot>() != null)
                {
                    _gunShoots[i] = _weaponContorls[i].GetComponentInChildren<GunShoot>();
                }

            }
        
    }
    private void Start()
    {
        _weaponContorls=FindObjectsOfType<WeaponContorl>();
        _gunShoots=new GunShoot[2];

        
    }
    public void GiveGranate()
    {
        _isHoldingGranade = true;
        _granadeObject = Instantiate(_granadePrefab,transform.position,transform.rotation);
        _granadeAnimator=_granadeObject.GetComponentInChildren<Animator>();
        
        _granade = _granadeObject.GetComponent<Granade>();
        for(int i = 0; i<_gunShoots.Length; i++)
        {
            if (_gunShoots[i] != null)
            {
                _gunShoots[i].EqupGranade();
            }
        }
    }

    public void GiveWeapon(int index)
    {

    }
    public void ThrowGaranade(Vector3 point)
    {
        if(_granadeAnimator != null)
        {
            _granadeAnimator.Play("Throw");
        }
        
        StartCoroutine(Wait(point));
        
    }
    IEnumerator Wait(Vector3 point)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        _isHoldingGranade = false;
        if (_granade != null)
        {
            _granade.Therow(point);
        }
        
        StopCoroutine(Wait(point));
    }
   
}
