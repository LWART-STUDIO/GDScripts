using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public bool Run;
    public bool Jump;
    public bool InTrigger;
    public bool IsDead;

    [SerializeField] private Vector3 _rotrateRight;
    [SerializeField] private Vector3 _rotrateLeft;
    [SerializeField] private float _shakeSpeed;
    [SerializeField] private Animator _bodyAnimator;

    private bool _doRight=false;
    private bool _doLeft=true;
    private bool _doUp=false;
    private bool _doDown = true;
    private Animator _animator;
 


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (Run)
        {
            _animator.SetBool("Run", true);
        }
        else if(!Run)
        {
            _animator.SetBool("Run", false);
        }
        if (Jump)
        {
            //_animator.SetTrigger("Jump");
        }
        if (IsDead)
        {
            _bodyAnimator.Play("BodyDead");
        }

    }
}
