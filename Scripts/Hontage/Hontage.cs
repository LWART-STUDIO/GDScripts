using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hontage : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    private GameObject _deadPanel;
    [SerializeField] private Rigidbody[] _ragdollRigidBodys;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _showText=false;
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private SkinnedMeshRenderer[] _darkMaterials;
    [SerializeField] private SkinnedMeshRenderer[] _lightMaterials;
    [SerializeField] private Material _dethLightMaterial;
    [SerializeField] private Material _dethDarkMaterial;
    [SerializeField] private AudioSource[] _ohNoAudio;


    private void Start()
    {
        _helpPanel.SetActive(false);
        _health = _maxHealth;
        _animator = GetComponent<Animator>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollRigidBodys = rigidbodies;
        _deadPanel = FindObjectOfType<DeadPanelMarker>().gameObject;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            _ragdollRigidBodys[i] = rigidbodies[i];
            _ragdollRigidBodys[i].isKinematic = true;
        }
    }
    public void ShowHelpText()
    {
        if (_showText)
        {
            _helpPanel.SetActive(true);
        }
    }
    public void GetHit()
    {
        if (_health > 0)
        {
            _health--;
        }
        if(_health <= 0)
        {
            Dead();
        }
    }
    private void Dead()
    {
        if (_showText)
        {
            _helpPanel.SetActive(false);
        }
        
        if (_animator != null)
        {
            _animator.enabled = false;
        }

        for (int i = 0; i < _ragdollRigidBodys.Length; i++)
        {
            _ragdollRigidBodys[i].isKinematic = false;

            
        }
        for (int i = 0; i < _darkMaterials.Length; i++)
        {
            _darkMaterials[i].material = _dethDarkMaterial;
        }
        for (int i = 0; i < _lightMaterials.Length; i++)
        {
            _lightMaterials[i].material = _dethLightMaterial;
        }
        _ohNoAudio[Random.Range(0, _ohNoAudio.Length)].Play();
        StartCoroutine(Kill());

    }
    private IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        ProjectTools.SetChildrenActive(_deadPanel, true);
    }
}


