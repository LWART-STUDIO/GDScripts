using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public  EndLvLPanel EndLvLPanel;
    public string LevelName;

    private void Start()
    {
        SaveManager.instance.AllLevelCounter++;
        SaveManager.instance.Save();
        StatisticSender.instance.LevelStart();
    }
    public void LevelEnd()
    {
        StatisticSender.instance.LevelEnd();
    }
    public void RestartLevel()
    {
        StatisticSender.instance.RestartLvl();
    }
}
