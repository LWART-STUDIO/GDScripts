using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;

public class UniversalTrigger : MonoBehaviour
{   
    [Header("Точка куда смотреть")]
    [SerializeField] private Transform _pointToVew;
    private GameObject _viewPoint;
    [Header("Скорость поворота камеры")]
    [SerializeField] private float _speedToGo;

    [Space(4)]
    [HideInInspector]public bool KilledAll;


    private PlayerMover _playerMover;
    private GameObject _player;
    private bool _timer=false;


    
    private GameObject _deadScreen;
    [Header("Предыдущий тригер")]
    [SerializeField] private GameObject _previusTriger;

    [Space(4)]
    private BulletsControl _bulletsControl;
    private PlayerAnimator _playerAnimator;
    private TriggerCounter _triggerCounter;

    [BeginGroup("АНИМАЦИИ ДВУХ РУК")]
    [SerializeField] private bool TwoHandInCenter;
    [SerializeField] private bool TwoHandJump;
    [SerializeField] private bool TwoHandX;
    [SerializeField] private bool TwoHandLeft;
    [EndGroup]
    [SerializeField] private bool TwoHandRight;

    [Space(4)]
    [Header("У кого запускать анимацию при входе")]
    [SerializeField] private EnemyAnimationController[] _enemyAnimationController;
    [Header("У кого запускать нацеливание руки на персонажа")]
    [SerializeField] private EnemyIKLookAtPlayer[] _enemyIKLookAtPlayer;
    private bool _goIn=false;
    private float _time=0;
    [Header ("Заложники")]
    [SerializeField] private Hontage[] _hontages;
    private ShootAbility _shootAbility;
    [Header("Враги которые должны стрелять при выходе из тригера")]
    [SerializeField] private Enemy[] _enemiesToShoot;
    private MainViewPoint _mainViewPoint;
    private float _mainViewPointSpeed;


    public int HeadShotCounter;
    private EnemyCounter _enemyCounter;
    private GameObject _ilealMarker;
    private GameObject _coolMarker;
    private bool _textShowen=false;
    [SerializeField] private float _trigerEnterSpeed=0.15f;

    private CameraShaker _cameraShaker;
    private void Start()
    {
        _goIn = false;
        _bulletsControl = FindObjectOfType<BulletsControl>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        _triggerCounter = FindObjectOfType<TriggerCounter>();
        _deadScreen = FindObjectOfType<DeadPanelMarker>().gameObject;
        _mainViewPoint = FindObjectOfType<MainViewPoint>();
        _ilealMarker=FindObjectOfType<IdealMarker>().gameObject;
        _coolMarker=FindObjectOfType<CoolMarker>().gameObject;
        _enemyCounter=GetComponent<EnemyCounter>();
        _cameraShaker = FindObjectOfType<CameraShaker>();
    }
    private void OnTriggerEnter(Collider player)
    {

        
        if (player.TryGetComponent<ShootAbility>(out ShootAbility shootAbility))
        {
            _shootAbility = shootAbility;
            _goIn = true;
            for (int i =0; i < _enemyAnimationController.Length; i++)
            {
                _enemyAnimationController[i].PlayAnimation();
            }
            PlayAnimation();
            _bulletsControl.Visibale = true;
            _playerAnimator.SpeedReloud = 6;
            if (_previusTriger != null)
            {
                Destroy(_previusTriger);
            }
            
            _viewPoint = player.GetComponentInChildren<ViewPoint>().gameObject;
            _player = player.gameObject;
            _playerMover=player.GetComponent<PlayerMover>();
            _playerMover.DoRoation = false;
            if(_mainViewPoint != null)
            {
                _mainViewPoint.DoRotation = false;
            }
            
            KilledAll = false;
            shootAbility.GiveAbilityToShoot();

            StartCoroutine(TriggerEnterTimer());
        }
        if(player.TryGetComponent<MainViewPoint>(out MainViewPoint mainViewPoint))
        {
            Debug.Log("ViewPointIn");
            _mainViewPointSpeed = mainViewPoint.Speed;
            mainViewPoint.Speed = _mainViewPointSpeed * 0.9f;
        }
    }
    private void OnTriggerExit(Collider player)
    {
        
        if (player.TryGetComponent<ShootAbility>(out ShootAbility shootAbility))
        {
            _goIn = false;
            shootAbility.HandSwither = false;
            _playerAnimator.SpeedReloud = 1;
            _triggerCounter.CounterPlus();
            if (_playerAnimator.Reloded && _bulletsControl.CanShoot)
            {
                _bulletsControl.Reloud();
            }

            _bulletsControl.Visibale = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f * 0.02f;
            shootAbility.GetAbilityToShoot();
            if (!KilledAll)
            {
                _cameraShaker.Run = false;
                for (int i = 0; i < _enemiesToShoot.Length; i++)
                {
                    _enemiesToShoot[i].Shoot();
                }
                /*_playerMover.enabled = false;
                
                ProjectTools.SetChildrenActive(_deadScreen, true);
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;*/
                _playerMover.enabled = false;
                StartCoroutine(Faild());
            }
            else
            {
                if (!_textShowen)
                {
                    if (HeadShotCounter >= _enemyCounter.MaxCount)
                    {
                        ProjectTools.SetChildrenActive(_ilealMarker, true);
                    }
                    else
                    {
                        ProjectTools.SetChildrenActive(_coolMarker, true);
                    }
                    _textShowen = true;
                }
            }
           

        }
        if (player.TryGetComponent<MainViewPoint>(out MainViewPoint mainViewPoint))
        {
            mainViewPoint.Speed = _mainViewPointSpeed * 1;
        }


    }
    private void FixedUpdate()
    {
        if (_goIn)
        {
            _time += 0.08f * Time.fixedUnscaledDeltaTime;
            for(int i=0; i< _enemyIKLookAtPlayer.Length; i++)
            {
                _enemyIKLookAtPlayer[i].RightHandAimWeight = Mathf.Lerp(0, 0.2f, _time);
                _enemyIKLookAtPlayer[i].LeftHandAimWeight = Mathf.Lerp(0, 0.2f, _time);
            }
        }
        

        if (_viewPoint != null)
        {
            if (!KilledAll)
            {
                Quaternion OriginalRot = _player.transform.localRotation;
                _player.transform.LookAt(_pointToVew.position);
                Quaternion NewRot = _player.transform.localRotation;
                _player.transform.localRotation = OriginalRot;
                _player.transform.localRotation = Quaternion.Lerp(_player.transform.localRotation, NewRot, _speedToGo * Time.fixedUnscaledDeltaTime);
            }
            else
            {
                if (!_timer)
                {
                    
                    StartCoroutine(KillAllTimer());
                }
                
                if (_timer)
                {
                    
                    _playerAnimator.SpeedReloud = 1;
                    StopCoroutine(KillAllTimer());
                    _mainViewPoint.DoRotation = true;
                    // _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _playerMover.Rotation, _speedToGo*1.8f * Time.fixedUnscaledDeltaTime);
                    // if (_player.transform.rotation == _playerMover.Rotation)
                    //{
                    _viewPoint = null;
                        // _playerMover.DoRoation = true;
                      //  _mainViewPoint.DoRotation = true;
                   // }
  
                }
                
            }
        }
        
        
    }
    private void PlayAnimation()
    {
        if (TwoHandInCenter)
        {
            _playerAnimator.TwoHandInCenter = true;

        }
        else if (TwoHandJump)
        {
            _playerAnimator.TwoHandJump = true;
        }
        else if (TwoHandX)
        {
            _playerAnimator.TwoHandX = true;
        }
        else if (TwoHandLeft)
        {
            _playerAnimator.TwoHandLeft = true;
        }
        else if (TwoHandRight)
        {
            _playerAnimator.TwoHandRight = true;
        }
    }
    IEnumerator KillAllTimer()
    {
        
        if (!_timer)
        {
            
            yield return new WaitForSecondsRealtime(1.5f);
            _timer = true;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f * 0.02f;
            _shootAbility.GetAbilityToShoot();
        }
    }
    IEnumerator TriggerEnterTimer()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = _trigerEnterSpeed;
        Time.fixedDeltaTime = _trigerEnterSpeed * 0.02f;
        for(int i = 0; i < _hontages.Length; i++)
        {
            _hontages[i].ShowHelpText();
        }
        StopCoroutine(TriggerEnterTimer());
    }
    IEnumerator Faild()
    {
        
        yield return new WaitForSecondsRealtime(0.8f);
        _cameraShaker.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        _cameraShaker.IsDead = true;
        ProjectTools.SetChildrenActive(_deadScreen, true);
        
        //Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;
    }
}


