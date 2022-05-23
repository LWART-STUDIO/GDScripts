using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class ShootAbility : MonoBehaviour
{
    [SerializeField]private bool _canShoot=false;
    private PlayerInput _playerInput;
    [SerializeField] private GunShoot[] _gunShoot;
    [SerializeField] private Animator _animator;
    public bool HandSwither=false;
    private PlayerConector _conector;
    private BulletsControl _bulletsControl;

    private void Start()
    {
        _conector = GetComponent<PlayerConector>();
        _playerInput= GetComponent<PlayerInput>();
        _gunShoot=GetComponent<PlayerConector>().GunShootScript;
        _bulletsControl=FindObjectOfType<BulletsControl>();

    }

    public void GiveAbilityToShoot()
    {
        _canShoot = true;
    } 

    public void GetAbilityToShoot()
    {
        _canShoot = false;
    }

    private void Update()
    {
        
        if (_canShoot)
        {
            if (_playerInput.WantShoot)
            {
                
                if (_bulletsControl.CanShoot)
                {
                    if (_gunShoot[1] != null)
                    {
                        if (!_gunShoot[1].IsHoldingGranade)
                        {

                            _bulletsControl.Shoot();
                            if (!_conector.OneHand)
                            {
                                if (HandSwither == false)
                                {
                                    _animator.Play("Shoot right hand");
                                    if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                    {
                                        _gunShoot[1].Shoot(_playerInput.TapPosition);
                                        HandSwither = true;
                                    }
                                    else
                                    {
                                        _gunShoot[0].Shoot(_playerInput.TapPosition);
                                        HandSwither = true;
                                    }
                                }
                                else
                                {
                                    _animator.Play("Shoot left hand");
                                    if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                    {
                                        _gunShoot[0].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }
                                    else
                                    {
                                        _gunShoot[1].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }


                                }
                            }
                            else
                            {
                                _animator.Play("OneHandShoot");

                                if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                {
                                    _gunShoot[1].Shoot(_playerInput.TapPosition);
                                    HandSwither = false;
                                }
                                else
                                {
                                    _gunShoot[0].Shoot(_playerInput.TapPosition);
                                    HandSwither = false;
                                }

                            }
                        }
                        else
                        {
                            if (_gunShoot[1] != null)
                            {
                                _gunShoot[1].Shoot(_playerInput.TapPosition);
                            }
                            if (_gunShoot[0] != null)
                            {
                                _gunShoot[0].Shoot(_playerInput.TapPosition);
                            }
                            HandSwither = false;
                        }
                    }
                    else if (_gunShoot[0] != null)
                    {
                        if (!_gunShoot[0].IsHoldingGranade)
                        {

                            _bulletsControl.Shoot();
                            if (!_conector.OneHand)
                            {
                                if (HandSwither == false)
                                {
                                    _animator.Play("Shoot right hand");
                                    if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                    {
                                        _gunShoot[1].Shoot(_playerInput.TapPosition);
                                        HandSwither = true;
                                    }
                                    else
                                    {
                                        _gunShoot[0].Shoot(_playerInput.TapPosition);
                                        HandSwither = true;
                                    }
                                }
                                else
                                {
                                    _animator.Play("Shoot left hand");
                                    if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                    {
                                        _gunShoot[0].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }
                                    else
                                    {
                                        _gunShoot[1].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }


                                }
                            }
                            else
                            {

                                if (_gunShoot[1] != null)
                                {
                                    if (_gunShoot[1].GetComponentInParent<RightHandHolder>())
                                    {
                                        _animator.Play("OneHandShoot");
                                        _gunShoot[1].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }
                                    else
                                    {
                                        _animator.Play("OneHandShoot");
                                        _gunShoot[0].Shoot(_playerInput.TapPosition);
                                        HandSwither = false;
                                    }
                                }
                                else
                                {
                                    _animator.Play("OneHandShoot");
                                    _gunShoot[0].Shoot(_playerInput.TapPosition);
                                    HandSwither = false;
                                }

                            }
                        }
                        else
                        {
                            if (_gunShoot[1] != null)
                            {
                                _gunShoot[1].Shoot(_playerInput.TapPosition);
                            }
                            if (_gunShoot[0] != null)
                            {
                                _gunShoot[0].Shoot(_playerInput.TapPosition);
                            }
                            HandSwither = false;
                        }
                    }


                }




            }
        }
        if (_gunShoot[0] == null)
        {
            _gunShoot = GetComponent<PlayerConector>().GunShootScript;
        }
    }
}
