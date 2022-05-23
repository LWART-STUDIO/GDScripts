using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuAnimator : MonoBehaviour
{
    [SerializeField] private string[] _animations;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(_animations[Random.Range(0, _animations.Length)]);
    }
}
