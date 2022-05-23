using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    private const string MaxSdkKey = "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";
    private const string InterstitialAdUnitId = "db2cdebaa857c53b";
    private const string RewardedAdUnitId = "93b08fd4e34d162d";

    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    public int RewardedCanShow => rewardedRetryAttempt;
    public int InterstitialCanShow => interstitialRetryAttempt;

    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            // AppLovin SDK is initialized, configure and start loading ads.
            //Debug.Log("MAX SDK Initialized");

            InitializeInterstitialAds();
            InitializeRewardedAds();
        };

        MaxSdk.SetSdkKey("6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR");
        MaxSdk.InitializeSdk();
    }

    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }

    public void ShowVideo()
    {
        if (MyTimer.instance.Finished)
        {
            ShowInterstitial();
        }
    }

    public void ShowInterstitial()
    {
        StartCoroutine(IEShowAd());
    }

    private IEnumerator IEShowAd()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
        //Debug.Log("Interstitial loaded");

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        //Debug.Log("Interstitial dismissed");
        LoadInterstitial();
        MyTimer.instance.Reset();
        if (LevelInformator.instance.NextLevel)
        {
            StatisticSender.instance.AdsWatch("Interstitial", "LevelEnd");
            LevelInformator.instance.GetNextLevel();
        }
        if (LevelInformator.instance.Faild)
        {
            StatisticSender.instance.AdsWatch("Interstitial", "Faild");
            LevelInformator.instance.GetFaild();
        }

    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is clicked. Pre-load the next ad
        //Debug.Log("Interstitial clicked");
        LoadInterstitial();
    }

    private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad revenue paid. Use this callback to track user revenue.
        //Debug.Log("Interstitial revenue paid");

        // Ad revenue
        double revenue = adInfo.Revenue;

        // Miscellaneous data
        string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
        string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
        string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
        string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
    }

    private void InitializeRewardedAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += RewardedFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedDismissedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnAdReceivedRewardEvent;
        

        // Load the first interstitial
        LoadRewarded();
    }

    void LoadRewarded()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }

    public void ShowRewarded()
    {
        StartCoroutine(REWShowAd());
    }

    private IEnumerator REWShowAd()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(RewardedAdUnitId);
        }
    }

    private void OnRewardedLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
        //Debug.Log("Rewarded loaded");

        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Rewarded ad displayed");
    }

    private void OnRewardedFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

        //RewardedStatusText.text = "Load failed: " + errorInfo.Code + "\nRetrying in " + retryDelay + "s...";
        //Debug.Log("Rewarded failed to load with error code: " + errorInfo.Code);

        Invoke("LoadRewarded", (float)retryDelay);
    }

    private void RewardedFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        //Debug.Log("Rewarded failed to display with error code: " + errorInfo.Code);
        LoadRewarded();
    }

    private void OnRewardedDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Rewarded dismissed");
        LoadRewarded();
    }

    private void OnAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        MyTimer.instance.Reset();
        if (LevelInformator.instance.WantToGiveRocketLauncher)
        {
            LevelInformator.instance.DoGiveRocketLauncher();
            LevelInformator.instance.GetWantToGiveRocketLauncher();
            StatisticSender.instance.AdsWatch("Rewarded", "GiveRocket");

        }
        if (LevelInformator.instance.WantToGiveMoreMoney)
        {
            StatisticSender.instance.AdsWatch("Rewarded", "Give1000Money");
            LevelInformator.instance.DoGiveMoreMoney();
            LevelInformator.instance.GetWantToGiveMoreMoney();
        }
        if (LevelInformator.instance.WantToGiveX2Money)
        {
            StatisticSender.instance.AdsWatch("Rewarded", "GiveX2Money");
            LevelInformator.instance.DoGiveX2Money();
            LevelInformator.instance.GetWantToGiveX2Money();
        }

        //Debug.Log("Rewarded received");
        //���� ������� �� REWARDED �����
        //���� ������� �� REWARDED �����
        //���� ������� �� REWARDED �����
        //���� ������� �� REWARDED �����
        //���� ������� �� REWARDED �����
    }
    

    private void OnRewardedClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Rewarded clicked");
        LoadRewarded();
    }

    private void OnRewardedRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad revenue paid. Use this callback to track user revenue.
        //Debug.Log("Rewarded revenue paid");

        // Ad revenue
        double revenue = adInfo.Revenue;

        // Miscellaneous data
        string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
        string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
        string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
        string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
    }

    #endregion
}