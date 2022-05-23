using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private PlayerConector _conector;
    [BeginGroup("ÀÍÈÌÀÖÈÈ ÄÂÓÕ ÐÓÊ")]
    public bool TwoHandInCenter;
    public bool TwoHandJump;
    public bool TwoHandX;
    public bool TwoHandLeft;
    [EndGroup]
    public bool TwoHandRight;
    [SerializeField] private float _speed=1;
    public float SpeedReloud;
    public bool Reloud;
    [SerializeField] private AudioSource[] _audioSource;
    private bool _reloded=true;
    public bool Reloded => _reloded;
    private ReloudEnded _relodedEnded;
    public bool ThrowGranade;
    private WeaponContorl[] _weaponContorl;
    private void Start()
    {
        _weaponContorl=GetComponentsInChildren<WeaponContorl>();
        _conector = GetComponent<PlayerConector>();
        _relodedEnded=FindObjectOfType<ReloudEnded>();
    }
    private void Update()
    {
        _animator.SetFloat("Speed", _speed);
        _animator.SetFloat("SpeedReloud", SpeedReloud);
        _reloded=_relodedEnded.Reloded;
        if (Reloud)
        {
            if (!_conector.OneHand)
            {
                if (_reloded)
                {
                    _audioSource[Random.Range(0, _audioSource.Length)].Play();
                    _animator.Play("TwoHandRecharge");
                    Reloud = false;
                }
            }
            if (_conector.OneHand)
            {
                _audioSource[Random.Range(0, _audioSource.Length)].Play();
                _animator.Play("OneHandRecharge");
                Reloud = false;
            }
             
            
        }
        if (!_weaponContorl[1].IsHoldingGranade&& !_weaponContorl[0].IsHoldingGranade)
        {
            if (!_conector.OneHand)
            {
                if (TwoHandInCenter)
                {
                    _animator.Play("TwoHandInCenter");
                    TwoHandInCenter = false;

                }
                else if (TwoHandJump)
                {
                    _animator.Play("TwoHandJump");
                    TwoHandJump = false;
                }
                else if (TwoHandX)
                {
                    _animator.Play("TwoHandX");
                    TwoHandX = false;
                }
                else if (TwoHandLeft)
                {
                    _animator.Play("TwoHandLeft");
                    TwoHandLeft = false;
                }
                else if (TwoHandRight)
                {
                    _animator.Play("TwoHandRight");
                    TwoHandRight = false;
                }


            }
            if (_conector.OneHand)
            {
                if (TwoHandInCenter)
                {
                    _animator.Play("OneHandInCenter");
                    TwoHandInCenter = false;

                }
                else if (TwoHandJump)
                {
                    _animator.Play("OneHandJump");
                    TwoHandJump = false;
                }
                else if (TwoHandX)
                {
                    _animator.Play("OneHandX");
                    TwoHandX = false;
                }
                else if (TwoHandLeft)
                {
                    _animator.Play("OneHandLeft");
                    TwoHandLeft = false;
                }
                else if (TwoHandRight)
                {
                    _animator.Play("OneHandRight");
                    TwoHandRight = false;
                }
            }


        }
        if (ThrowGranade)
        {
            if (!_conector.OneHand)
            {
                _animator.Play("GranadeThrow");
                ThrowGranade = false;
            }
            if (_conector.OneHand)
            {

                _animator.Play("GranadeLeft");
                ThrowGranade = false;
            }
        }
    }
}

