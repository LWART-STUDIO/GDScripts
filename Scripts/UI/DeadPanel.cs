using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanel : MonoBehaviour
{
    private ShootAbility _shootAbility;
    private PlayerMover _playerMover;
   [SerializeField] private Animator _animator;
    private CameraShaker _cameraShaker;
    
    
    private void Start()
    {
        _animator = FindObjectOfType<ReloudEnded>().GetComponent<Animator>();
        gameObject.SetActive(false);
        _cameraShaker = FindObjectOfType<CameraShaker>();
    }
    private void OnEnable()
    {
        _shootAbility=FindObjectOfType<ShootAbility>();
        _playerMover=FindObjectOfType<PlayerMover>();
        //_animator=FindObjectOfType<ReloudEnded>().GetComponent<Animator>();
    }
    private void Update()
    {
        
        if (_playerMover.GetComponent<Rigidbody>().isKinematic != false)
        {
            _playerMover.GetComponent<Rigidbody>().isKinematic = false;
            _animator = FindObjectOfType<ReloudEnded>().GetComponent<Animator>();
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _playerMover.enabled = false;
            _shootAbility.GetAbilityToShoot();
            
            _cameraShaker.IsDead = true;
            StartCoroutine(DeadTime());
        }
        
        
    }
    private IEnumerator DeadTime()
    {
        yield return new WaitForSecondsRealtime(0f);
        _animator.Play("TwoHandDeath");
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0f;
    }
}
