using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsShower : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjectsToDeactivate;
    [SerializeField] private GameObject _loadingSpin;
    [SerializeField] private Button _button;

    
    private void Start()
    {
        if(_button != null)
        {
            _button.interactable = false;
        }
        if(_loadingSpin != null)
        {
            _loadingSpin.SetActive(true);
        }
        for (int i = 0; i < _gameObjectsToDeactivate.Length; i++)
        {
            _gameObjectsToDeactivate[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (MaxSdk.IsRewardedAdReady("93b08fd4e34d162d"))
        {
            if (_button != null)
            {
                _button.interactable = true;
            }
            if (_loadingSpin != null)
            {
                _loadingSpin.SetActive(false);
            }
            for (int i = 0; i < _gameObjectsToDeactivate.Length; i++)
            {
                _gameObjectsToDeactivate[i].SetActive(true);
            }
        }
        else if(MaxSdk.IsInterstitialReady("db2cdebaa857c53b"))
        {
            if (_button != null)
            {
                _button.interactable = true;
            }
            if (_loadingSpin != null)
            {
                _loadingSpin.SetActive(false);
            }
            for (int i = 0; i < _gameObjectsToDeactivate.Length; i++)
            {
                _gameObjectsToDeactivate[i].SetActive(true);
            }
        }
        else
        {
            if (_button != null)
            {
                _button.interactable = false;
            }
            _loadingSpin.SetActive(true);
            for (int i = 0; i < _gameObjectsToDeactivate.Length; i++)
            {
                _gameObjectsToDeactivate[i].SetActive(false);
            }
        }
    }
    public void ShowAdForRocketLauncher()
    {
        if (MaxSdk.IsRewardedAdReady("93b08fd4e34d162d"))
        {
            LevelInformator.instance.DoWantToGiveRocketLauncher();
            AdsManager.instance.ShowRewarded();
        }
        /*else if (MaxSdk.IsInterstitialReady("9d3b46093120fddb"))
        {
            AdsManager.instance.ShowInterstitial();
        }*/
    }
    public void ShowAdForMoreMoney()
    {
        if (MaxSdk.IsRewardedAdReady("93b08fd4e34d162d"))
        {
            LevelInformator.instance.DoWantToGiveMoreMoney();
            AdsManager.instance.ShowRewarded();
        }
        /*else if (MaxSdk.IsInterstitialReady("9d3b46093120fddb"))
        {
            AdsManager.instance.ShowInterstitial();
        }*/
    }
    public void ShowAdForX2Money()
    {
        if (MaxSdk.IsRewardedAdReady("93b08fd4e34d162d"))
        {
            LevelInformator.instance.DoWantToGiveX2Money();
            AdsManager.instance.ShowRewarded();
        }
        /*else if (MaxSdk.IsInterstitialReady("9d3b46093120fddb"))
        {
            AdsManager.instance.ShowInterstitial();
        }*/
    }
    public void ShowIntestitial()
    {
        if (MyTimer.instance.Finished)
        {
            if (MaxSdk.IsInterstitialReady("db2cdebaa857c53b"))
            {
                AdsManager.instance.ShowInterstitial();
            }
        }
    }
    public void ShowRewarded()
    {
        if (MaxSdk.IsRewardedAdReady("93b08fd4e34d162d"))
        {
            AdsManager.instance.ShowRewarded();
        }
    }
}
