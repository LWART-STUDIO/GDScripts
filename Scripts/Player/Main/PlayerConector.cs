using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerConector : MonoBehaviour
{
    [BeginGroup("ÍÅÎÁÕÎÄÈÌÛÅ ÇÀÂÈÑÈÌÎÑÒÈ")]
    [SerializeField] private ShootAbility _shootAbility;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PathCreator _path;
    [SerializeField]private Animator _animator;
    [EndGroup]
    [SerializeField] private GunSelector _gunSelector;
    private GameObject[] _gunHolder=new GameObject[] {};
    private GunShoot[] _gunShoot;
    public GunShoot[] GunShootScript => _gunShoot;
    [BeginGroup("ÍÅÎÁÕÎÄÈÌÛÅ ÏÀÐÀÌÅÒÐÛ")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private bool _doRotationOnPath;
    [SerializeField] private GunsDataSo _gunsData;
    [EndGroup]
    [SerializeField] private EndOfPathInstruction _howWolkPath;
    [BeginGroup("ÇÀÏÓÑÊ ÌÅÒÎÄÎÂ")]
    [EditorButton(nameof(GiveAbilityToShoot), "Can Shoot", activityType: ButtonActivityType.OnPlayMode)]
    [EditorButton(nameof(GetAbilityToShoot), "Cant Shoot", activityType: ButtonActivityType.OnPlayMode)]
    [EndGroup]
    [EditorButton(nameof(GiveGranate), "GiveGranate", activityType: ButtonActivityType.OnPlayMode)]
    [SerializeField] private bool _bool;
    
    private WeaponContorl[] _weaponContorls;
    private bool _oneHand;
    
    public bool OneHand => _oneHand;
    public bool GiveRocketLauncher=true;
    public bool Starting=false;
    private int _gunIndex=-1;
    public int GunIndex=> _gunIndex;
    private bool _startSetUp=false;
    

    
    private void Awake()
    {
        _weaponContorls= FindObjectsOfType<WeaponContorl>();
        _startSetUp=false;
        SetSpeed(_playerSpeed);
        SetDoRotation(_doRotationOnPath);
        SetHowWolkOnPath(_howWolkPath);
        SetPath(_path);
        _gunHolder = new GameObject[2] { _weaponContorls[0].gameObject, _weaponContorls[1].gameObject };
        SetGunHolder(_gunHolder);
        _gunShoot = new GunShoot[2] { _gunHolder[0].GetComponentInChildren<GunShoot>(), _gunHolder[1].GetComponentInChildren<GunShoot>() };

    }
    private void Start()
    {
        

    }
    public void GiveAbilityToShoot()
    {
        _shootAbility.GiveAbilityToShoot();
    }
    public void GetAbilityToShoot()
    {
        _shootAbility.GetAbilityToShoot();
    }
    public void SetPath(PathCreator pathCreator)
    {
        _playerMover.SetPath(pathCreator);
    }
    public void SetHowWolkOnPath(EndOfPathInstruction endOfPathInstruction)
    {
        _playerMover.SetHowWolkOnPath(endOfPathInstruction);
    }
    public void SetSpeed(float speed)
    {
        _playerMover.SetSpeed(speed);
    }
    public void SetDoRotation(bool doRotation)
    {
        _playerMover.SetDoRotation(doRotation);

    }
    public void SetSkin(GameObject gunPrefub)
    {
        
        _gunSelector.GenerateGun(gunPrefub);
    }
    public void RemoveSkin()
    {
        _gunSelector.DestroyGun();
    }
    public void SetGunHolder(GameObject[] gunHolder)
    {
        _gunSelector.SetGunHoler(gunHolder);
    }
    public void GiveGranate()
    {
        if (!OneHand)
        {
            _weaponContorls[1].GiveGranate();
        }
        else
        {
            
            _weaponContorls[0].GiveGranate();
        }
        
    }
    private void Update()
    {
        Debug.Log(_oneHand);
        if (!_startSetUp)
        {
            int curentSkin = SaveManager.instance.CurrentGun;
            _gunIndex = curentSkin;
            _oneHand = _gunsData.GunsData[curentSkin].GunData.Gun.OneHand;

            SetSkin(_gunsData.GunsData[curentSkin].GunData.Gun.Prefub);
            _startSetUp = true;
        }
        if (_gunShoot[0] == null)
        {
            _gunShoot = new GunShoot[2] { _gunHolder[0].GetComponentInChildren<GunShoot>(), _gunHolder[1].GetComponentInChildren<GunShoot>() };

        }
    }
    public void GiveSavedGun()
    {
        if (_gunShoot[0] != null)
        {
            RemoveSkin();
        }
        int curentSkin = SaveManager.instance.CurrentGun;
        _gunIndex = curentSkin;
        _oneHand = _gunsData.GunsData[_gunIndex].GunData.Gun.OneHand;
        _animator.SetBool("OneHand", _oneHand);
        SetSkin(_gunsData.GunsData[_gunIndex].GunData.Gun.Prefub);
        Starting = false;
    }
    public void GiveRocket()
    {
        if (_gunShoot[0] != null)
        {
            RemoveSkin();
        }
        int curentSkin1 = 12;
        _gunIndex = curentSkin1;
        _oneHand = _gunsData.GunsData[_gunIndex].GunData.Gun.OneHand;
        _animator.SetBool("OneHand", _oneHand);
        SetSkin(_gunsData.GunsData[_gunIndex].GunData.Gun.Prefub);
        Starting = false;
    }
    

}
