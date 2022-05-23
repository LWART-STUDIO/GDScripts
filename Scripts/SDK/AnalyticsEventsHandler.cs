using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsEventsHandler : MonoBehaviour
{

    public static AnalyticsEventsHandler instance { get; private set; }

    void Awake()
    {
        //инициализация рекламного SDK
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {

        };

        MaxSdk.SetSdkKey("6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR");
        MaxSdk.InitializeSdk();

        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        if (!FB.IsInitialized)
        {
            // Инициализация SDK FB аналитики
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Активация приложения, если SDK уже инициализировано
            FB.ActivateApp();
        }
    }

    //публичный метод вызываемый другим скриптом, когда мы начинаем уровень (счётчик старта уровней)
    public void LevelStart(int start)
    {
        var lvlStart = new Dictionary<string, object>();
        lvlStart["Start"] = start.ToString();

        FB.LogAppEvent("Level Started", parameters: lvlStart);
    }

    public void SendEventStart(int level_number, string level_name, int level_count, int level_loop)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data["level_number"] = level_number;
        data["level_name"] = level_name;
        data["level_count"] = level_count;
        data["level_loop"] = level_loop;

        AppMetrica.Instance.ReportEvent("level_start", data);
        AppMetrica.Instance.SendEventsBuffer();
    }

    //публичный метод вызываемый другим скриптом, когда мы падаем в пропасть (счётчик смертей)
    public void Dead(int dead)
    {
        var death = new Dictionary<string, object>();
        death["Lose"] = dead.ToString();

        FB.LogAppEvent("Lose Level", parameters: death);
    }

    //публичный метод вызываемый другим скриптом, когда мы завершаем уровень (счётчик пройденный уровней)
    public void EndLevel(int end)
    {
        var fin = new Dictionary<string, object>();
        fin["EndLvl"] = end.ToString();

        FB.LogAppEvent("Level Complite", parameters: fin);
    }

    public void SendEventEnd(int level_number, string level_name, int level_count, int level_loop, string result, int time)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data["level_number"] = level_number;
        data["level_name"] = level_name;
        data["level_count"] = level_count;
        data["level_loop"] = level_loop;
        data["result"] = result;
        data["time"] = time;

        AppMetrica.Instance.ReportEvent("level_finish", data);
        AppMetrica.Instance.SendEventsBuffer();

    }

    public void AdsWathed(string ad_type,string placemant, int level_number, string level_name, int level_count, int level_loop)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data["ad_type"] = ad_type;
        data["placement"]=placemant;
        data["level_number"]=level_number;
        data["level_name"]= level_name;
        data["level_count"] = level_count;
        data["level_loop"] = level_loop;
        AppMetrica.Instance.ReportEvent("video_ads_watch", data);
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Активация приложения
            FB.ActivateApp();
        }
        else
        {
            // Вывод ошибки в консоль Unity в случае неудачной инициализации SDK
            Debug.LogError("Failed to Initialize the Facebook SDK");
        }
    }




    //публичный метод вызываемый другим скриптом "готов ли пользователь смотреть видео"
    //public void VideoAdsAvaliable(string type, string placement, string result, bool connection)
    //{
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    data["ad_type"] = type;
    //    data["placement"] = placement;
    //    data["result"] = result;
    //    data["connection"] = connection ? "true" : "false";

    //    AppMetrica.Instance.ReportEvent("video_ads_available", data);
    //}

    //публичный метод вызываемый другим скриптом "видео запустилось"
    //public void VideoAdsStarted(string type, string placement, string result, bool connection)
    //{
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    data["ad_type"] = type;
    //    data["placement"] = placement;
    //    data["result"] = result;
    //    data["connection"] = connection ? "true" : "false";

    //    AppMetrica.Instance.ReportEvent("video_ads_started", data);
    //}

    //публичный метод вызываемый другим скриптом "результат просмотра видео"
    //public void VideoAdsWatch(string type, string placement, string result, bool connection,
    //                          int level_number, string level_name, int level_count, int level_loop)
    //{
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    data["ad_type"] = type;
    //    data["placement"] = placement;
    //    data["result"] = result;
    //    data["connection"] = connection ? "true" : "false";
    //    data["level_number"] = level_number;
    //    data["level_name"] = level_name;
    //    data["level_count"] = level_count;
    //    data["level_loop"] = level_loop;

    //    AppMetrica.Instance.ReportEvent("video_ads_watch", data);
    //}


    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Ставим на паузу игру при сворачивании приложения
            Time.timeScale = 0;
        }
        else
        {
            // Возобновляем игру при разворачивании приложения
            Time.timeScale = 1;
        }
    }
}