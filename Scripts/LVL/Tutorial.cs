using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool _tutorialEnd;
    private bool _goIn;
    [SerializeField] private GameObject _tutorialCanvas;
    private UniversalTrigger _universalTrigger;

    private void Start()
    {
        if (_tutorialCanvas != null)
        {
            _tutorialCanvas.SetActive(false);
        }
        _universalTrigger = GetComponent<UniversalTrigger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ShootAbility>(out ShootAbility shootAbility))
        {
            _goIn = true;
        }
    }
    private void FixedUpdate()
    {
        if (SaveManager.instance.TutorialEnded == false)
        {
            if (_goIn && !_tutorialEnd)
            {
                if (_tutorialCanvas != null)
                {
                    _tutorialCanvas.SetActive(true);
                }
            }
            else
            {
                if (_tutorialCanvas != null)
                {
                    _tutorialCanvas.SetActive(false);
                }
            }
            if (_universalTrigger.KilledAll == true)
            {
                _tutorialEnd = true;
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ShootAbility>(out ShootAbility shootAbility))
        {
            _goIn = false;
            _tutorialEnd = true;
        }
    }

}
