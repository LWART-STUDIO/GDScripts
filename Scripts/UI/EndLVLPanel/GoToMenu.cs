using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
   public void LoadMenu()
    {
        
        LoadingScreen.instance.LoadScene(0);
    }
}
