using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private PlayerMover _playerMover;
    private CameraShaker _cameraShaker;
    [SerializeField] private Animator _handsAnimator;
    private PlayerJumper _jumper;

    private void Start()
    {
        _handsAnimator=FindObjectOfType<ReloudEnded>().GetComponent<Animator>();
        _cameraShaker = FindObjectOfType<CameraShaker>();
        _jumper = FindObjectOfType<PlayerJumper>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
        {
            _cameraShaker.Run = false;
            _handsAnimator.SetFloat("WalkSpeed", 0);
            _cameraShaker.Jump = true;
            _jumper.Jump = true;
            
            
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
        {
            _cameraShaker.Run = true;
            _handsAnimator.SetFloat("WalkSpeed", 1);
            _cameraShaker.Jump = true;
            _jumper.Jump = true;
            
        }
    }
    private void FixedUpdate()
    {
       
        
    }
}
