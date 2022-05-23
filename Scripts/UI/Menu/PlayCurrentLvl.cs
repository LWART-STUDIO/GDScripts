using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCurrentLvl : MonoBehaviour
{
   
    public void PlayLvl()
    {
        LoadingScreen.instance.LoadScene(SaveManager.instance.CurrentLvl);
    }
}
