using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    private PlayerConector _conector;
    private GameObject[] _gunHolder;

    private void Start()
    {
        _conector = GetComponent<PlayerConector>();
    }
    public void GenerateGun(GameObject gunPregub)
    {
        if (!_conector.OneHand)
        {
            for (int i = 0; i < _gunHolder.Length; i++)
            {
                Instantiate(gunPregub, _gunHolder[i].transform);
            }
        }
        else
        {
            if(_gunHolder[1].TryGetComponent<RightHandHolder>(out RightHandHolder rightHandHolder))
            {
                Instantiate(gunPregub, _gunHolder[1].transform);
            }
            else
            {
                Instantiate(gunPregub, _gunHolder[0].transform);
            }
                    
            
        }
        
        
    }
    public void DestroyGun()
    {
        if (!_conector.OneHand)
        {
            for (int i = 0; i < _gunHolder.Length; i++)
            {
                
                Destroy(_gunHolder[i].GetComponentInChildren<GunShoot>().gameObject);
            }
        }
        else
        {
            if (_gunHolder[1].TryGetComponent<RightHandHolder>(out RightHandHolder rightHandHolder))
            {
               
                Destroy(_gunHolder[1].GetComponentInChildren<GunShoot>().gameObject);
            }
            else
            {
                
                Destroy(_gunHolder[0].GetComponentInChildren<GunShoot>().gameObject);
            }


        }


    }
    public void SetGunHoler(GameObject[] gunHolder)
    {
        _gunHolder = gunHolder;
    }
}
