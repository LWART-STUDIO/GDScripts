using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticSender : MonoBehaviour
{
    public static StatisticSender instance { get; private set; }
    [SerializeField] private EndLvLPanel _endLvLPanel;
    [SerializeField] private string _levelName;
    private string _result;
    private int _timeLeft;
    private float _time;
    private LevelInfo _levelInfo;
    [SerializeField]private int _levelNuber;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LevelStart()
    {
        
        AnalyticsEventsHandler.instance.LevelStart(_levelNuber);
        AnalyticsEventsHandler.instance.SendEventStart(SaveManager.instance.CurrentLevelCounter, _levelName, SaveManager.instance.AllLevelCounter, 0);
    }
    public void LevelEnd()
    {
        _result = "win";
        LevelInformator.instance.DoNextLevel();
        AnalyticsEventsHandler.instance.EndLevel(_levelNuber);
        AnalyticsEventsHandler.instance.SendEventEnd(SaveManager.instance.CurrentLevelCounter, _levelName, SaveManager.instance.AllLevelCounter, 0, _result, _timeLeft);
    }
    public void RestartLvl()
    {
        _result = "lose";
        LevelInformator.instance.DoFaild();
        AnalyticsEventsHandler.instance.Dead(_levelNuber);
        AnalyticsEventsHandler.instance.SendEventEnd(SaveManager.instance.CurrentLevelCounter, _levelName, SaveManager.instance.AllLevelCounter, 0, _result, _timeLeft);
    }
    public void AdsWatch(string ad_type,string placemant)
    {
        AnalyticsEventsHandler.instance.AdsWathed(ad_type,placemant, _levelNuber, _levelName,SaveManager.instance.CurrentLevelCounter, SaveManager.instance.AllLevelCounter);
    }
    private void Update()
    {
        if (_levelInfo == null)
        {
            _levelInfo=FindObjectOfType<LevelInfo>();
            _endLvLPanel = _levelInfo.EndLvLPanel;
            _levelName= _levelInfo.LevelName;
            _levelNuber = _endLvLPanel.CurrentLvlIndex;
        }
        _time+=Time.unscaledDeltaTime;
        _timeLeft=(int)System.Math.Round(_time);
    }
}
