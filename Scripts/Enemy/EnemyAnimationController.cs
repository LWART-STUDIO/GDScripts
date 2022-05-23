using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyAnimationController : MonoBehaviour
{

    [BeginGroup("Анимация покоя")]
    [SerializeField] private bool _idle1;
    [SerializeField] private bool _idle2;
    [SerializeField] private bool _idle3;
    [SerializeField] private bool _idle4;
    [SerializeField] private bool _idle5;
    [SerializeField] private bool _idle6;
    [SerializeField] private bool _idle7;
    [EndGroup]
    [SerializeField] private bool _idleKamikadze;
    [Space(3)]
    [BeginGroup("Анимация при входе в тригер")]
    [SerializeField] private bool _jump;
    [SerializeField] private bool _jumpWithTwist;
    [SerializeField] private bool _idleToAim;
    [SerializeField] private bool _runToAim;
    [SerializeField] private bool _seatToAim;
    [SerializeField] private bool _ropeSwing;
    [SerializeField] private bool _floorSlip;
    [SerializeField] private bool _jumpInSide;
    [SerializeField] private bool _granadeThrow;
    [SerializeField] private bool _jumpInSideRight;
    [SerializeField] private bool _bazukaSeat;
    [SerializeField] private bool _bazukaStand;
    public bool GranadeThrow => _granadeThrow;
    [EndGroup]
    [SerializeField] private bool _runFamikadze;
    [Space(3)]

    private bool _goToPath;
    [BeginGroup("Следование по пути")]
    [Header("Скорость движения")]
    [SerializeField] private float _speed;
    [Header("Путь")]
    [SerializeField] private PathCreator _path;
    [Header("Что делать в конце пути")]
    [SerializeField] private EndOfPathInstruction _end;
    [EndGroup]
    [Header("Повторять вращение за путем")]
    [SerializeField] private bool _doRotation;
    GranadeThrow _granadeThrowComponent;
    [Header("Компанент запуска ракеты")]
    [SerializeField] private RoketLaunh _roketLaunh;

    private bool _mix = false;
    private float _distanceGone;
    private Quaternion _rotation;

    private float _time;
    [BeginGroup]
    [Header("Индикатор стрельбы")]
    [SerializeField] private GameObject _warningMessege;
    private Enemy _enemy;
    private bool _showWarningIcon;
    [Header("Через какое время показывать индикатор стрельбы")]
    [EndGroup]
    [SerializeField] private float _timeToShowWarning;

    private Animator _mainAnimator;
    [SerializeField] private bool _quaracopeter;
    private void Awake()
    {
        if (_quaracopeter)
        {
            _mainAnimator = GetComponentInChildren<Animator>();
        }
        else
        {
            _mainAnimator = GetComponent<Animator>();
        }

        _warningMessege.SetActive(false);
        _enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        _granadeThrowComponent = GetComponent<GranadeThrow>();
        // PlayAnimation();
        IdleAnimation();
    }
    private void FixedUpdate()
    {
        if (_goToPath)
        {
            _distanceGone += _speed * Time.fixedDeltaTime;
            transform.position = _path.path.GetPointAtDistance(_distanceGone, _end);
            _rotation = _path.path.GetRotationAtDistance(_distanceGone, _end);

            if (_doRotation)
            {
                transform.rotation = _rotation;
            }
        }

    }
    public void IdleAnimation()
    {
        if (_idle1)
        {

            _mainAnimator.Play("Idle1");

        }
        if (_idle2)
        {

            _mainAnimator.Play("Idle2");


        }
        if (_idle3)
        {

            _mainAnimator.Play("Idle3");

        }
        if (_idle4)
        {

            _mainAnimator.Play("Idle4");

        }
        if (_idle5)
        {

            _mainAnimator.Play("Idle5");


        }
        if (_idle6)
        {

            _mainAnimator.Play("Idle6");

        }
        if (_idle7)
        {

            _mainAnimator.Play("Idle7");

        }
        if (_idleKamikadze)
        {

            _mainAnimator.Play("IdleKamikadze");

        }
    }
    public void PlayAnimation()
    {
        if (_jump)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Jump");
            _jump = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_jumpWithTwist)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Jump with twist");
            _jumpWithTwist = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_idleToAim)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Idle to aim");
            _idleToAim = false;
            if (_roketLaunh != null)
            {
                _roketLaunh.Lanch = true;
            }
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_runToAim)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Run to aim");
            _runToAim = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_seatToAim)
        {
            StartCoroutine(ShowWarningIcon());
            if (_roketLaunh != null)
            {
                _roketLaunh.Lanch = true;
            }
            _mix = true;
            _mainAnimator.Play("Seat to aim");
            _seatToAim = false;
            if (_path != null)
            {
                _goToPath = true;
            }

        }
        if (_ropeSwing)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Rope swing");
            _ropeSwing = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_jumpInSideRight)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Jump in side right");
            _jumpInSideRight = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_floorSlip)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Floor slip");
            _floorSlip = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_jumpInSide)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("Jump in side");
            _jumpInSide = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_runFamikadze)
        {
            _mix = true;
            _mainAnimator.Play("RunKamikadze");
            _runFamikadze = false;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_granadeThrow)
        {
            StartCoroutine(ShowWarningIcon());
            _mix = true;
            _mainAnimator.Play("GranadeThrow");
            _runFamikadze = false;
            _granadeThrowComponent.Throw = true;
            if (_path != null)
            {
                _goToPath = true;
            }
        }
        if (_bazukaSeat)
        {
            StartCoroutine(ShowWarningIcon());
            if (_roketLaunh != null)
            {
                _roketLaunh.Lanch = true;
            }
            _mix = true;
            _mainAnimator.Play("Basuka seat");
            _bazukaSeat = false;
            if (_path != null)
            {
                _goToPath = true;
            }

        }
        if (_bazukaStand)
        {
            StartCoroutine(ShowWarningIcon());
            if (_roketLaunh != null)
            {
                _roketLaunh.Lanch = true;
            }
            _mix = true;
            _mainAnimator.Play("Basuka stand");
            _bazukaStand = false;
            if (_path != null)
            {
                _goToPath = true;
            }

        }

    }
    private void Update()
    {
        if (_mix)
        {

            _time += 1 * Time.unscaledDeltaTime;
            _mainAnimator.SetLayerWeight(1, Mathf.Lerp(1, 0, _time));
        }
    }
    IEnumerator ShowWarningIcon()
    {
        yield return new WaitForSeconds(_timeToShowWarning);
        if (_enemy.IsAlive)
        {
            _warningMessege.SetActive(true);
        }

    }
}
