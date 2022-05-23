using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coffee.UIExtensions;

public class GiveX2Money : MonoBehaviour
{
    [SerializeField] private EndLvlTrigger _endLvlTrigger;
    [SerializeField] private TMP_Text _text;
    private UIParticle _particleSystem;

    private void Start()
    {
        _endLvlTrigger=FindObjectOfType<EndLvlTrigger>();
    }

    public void Activate()
    {
        if (_endLvlTrigger != null)
        {
            SaveManager.instance.Money+=_endLvlTrigger.MoneyToGive;
            gameObject.SetActive(false);
            if (_text != null)
            {
                _text.text="+"+ (_endLvlTrigger.MoneyToGive*2);
            }
        }
    }
    public void GiveMoreMoney()
    {
        SaveManager.instance.Money += 1000;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (LevelInformator.instance.GiveMoreMoney)
        {
            _particleSystem=FindObjectOfType<MoneyParticalMarker>().GetComponent<UIParticle>();
            _particleSystem.Play();
            GiveMoreMoney();
            LevelInformator.instance.GetGiveMoreMoney();
        }
        if (LevelInformator.instance.GiveX2Money)
        {
            _particleSystem = FindObjectOfType<MoneyParticalMarker>().GetComponent<UIParticle>();
            _particleSystem.Play();
            Activate();
            LevelInformator.instance.GetGiveX2Money();
        }
    }
}

