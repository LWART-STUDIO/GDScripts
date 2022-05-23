using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloder : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadScene()
    {
        LoadingScreen.instance.LoadScene(_sceneIndex);

    }
}
