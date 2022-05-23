using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuGunSwither : MonoBehaviour
{
    [SerializeField] private GameObject _leftGunHolder;
    [SerializeField] private GameObject _rightGunHolder;
    [SerializeField] private GameObject _leftGun;
    [SerializeField] private GameObject _rightGun;
    [SerializeField] private GunsDataSo _data;
    private bool _oneHand;

    private void Update()
    {
        _oneHand = _data.GunsData[SaveManager.instance.CurrentGun].GunData.Gun.OneHand;
        if(_rightGun!= _data.GunsData[SaveManager.instance.CurrentGun].GunData.Gun.Prefub)
        {
            if (_oneHand)
            {
                Destroy(_rightGun);
                Destroy(_leftGun);
                _rightGun = Instantiate(_data.GunsData[SaveManager.instance.CurrentGun].GunData.Gun.Prefub, _rightGunHolder.transform);
            }
            else
            {
                Destroy(_rightGun);
                Destroy(_leftGun);
                _rightGun = Instantiate(_data.GunsData[SaveManager.instance.CurrentGun].GunData.Gun.Prefub, _rightGunHolder.transform);
                _leftGun = Instantiate(_data.GunsData[SaveManager.instance.CurrentGun].GunData.Gun.Prefub, _leftGunHolder.transform);
            }
        }
        
    }
}
