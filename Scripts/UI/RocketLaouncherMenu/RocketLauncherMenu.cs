using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class RocketLauncherMenu : MonoBehaviour
{
    private bool _makeDesision=false;
    private PlayerConector _conector;

    private void Awake()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        _conector =FindObjectOfType<PlayerConector>();
        LevelInformator.instance.NewLevelStart();
    }
    
    public void GiveGift()
    {
        _conector = FindObjectOfType<PlayerConector>();
        _conector.GiveRocketLauncher = true;
        _makeDesision = true;
        _conector.Starting = true;
        _conector.GiveRocket();
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f * 0.02f;
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        _conector = FindObjectOfType<PlayerConector>();
        _conector.GiveRocketLauncher = false;
        _makeDesision = true;
        _conector.Starting = true;
        _conector.GiveSavedGun();
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f * 0.02f;
    }
    private void Update()
    {
        if (_makeDesision == false)
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }
        if (LevelInformator.instance.GiveRocketLauncher)
        {
            
            GiveGift();
            LevelInformator.instance.GetGiveRocketLauncher();
            LevelInformator.instance.GetWantToGiveRocketLauncher();
        }
        //Time.timeScale = 0;
    }
}
