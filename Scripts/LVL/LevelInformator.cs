using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformator : MonoBehaviour
{
    public static LevelInformator instance { get; set; }
    public bool GiveRocketLauncher;
    public bool GiveMoreMoney;
    public bool GiveX2Money;

    public bool WantToGiveRocketLauncher;
    public bool WantToGiveMoreMoney;
    public bool WantToGiveX2Money;

    public bool NextLevel;
    public bool Faild;
    


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewLevelStart()
    {
        GiveRocketLauncher = false;
        GiveMoreMoney = false;
        GiveX2Money = false;
        NextLevel = false;
        Faild = false;
    }
    public void DoGiveRocketLauncher()
    {
        GiveRocketLauncher=true;
    }
    public void DoGiveMoreMoney()
    {
        GiveMoreMoney = true;
    }
    public void DoGiveX2Money()
    {
        GiveX2Money=true;
    }

    public void DoNextLevel()
    {
        NextLevel = true;
    }
    public void DoFaild()
    {
        Faild = true;
    }

    public void GetGiveRocketLauncher()
    {
        GiveRocketLauncher = false;
    }
    public void GetGiveMoreMoney()
    {
        GiveMoreMoney = false;
    }
    public void GetGiveX2Money()
    {
        GiveX2Money = false;
    }

    public void GetNextLevel()
    {
        NextLevel = false;

    }
    public void GetFaild()
    {
        Faild = false;
    }



    public void DoWantToGiveRocketLauncher()
    {
        WantToGiveRocketLauncher = true;
    }
    public void DoWantToGiveMoreMoney()
    {
        WantToGiveMoreMoney = true;
    }
    public void DoWantToGiveX2Money()
    {
        WantToGiveX2Money = true;
    }


    public void GetWantToGiveRocketLauncher()
    {
        WantToGiveRocketLauncher = false;
    }
    public void GetWantToGiveMoreMoney()
    {
        WantToGiveMoreMoney = false;
    }
    public void GetWantToGiveX2Money()
    {
        WantToGiveX2Money = false;
    }

}
