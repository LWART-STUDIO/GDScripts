using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private Image[] _levelPoints;
    [SerializeField] private GameObject[] _currentLvlMarker;
    [SerializeField] private Image[] _levelsImage;
    [SerializeField] private Sprite[] _levelSprites;
    [SerializeField] private Sprite[] _pointSprites;

    private void Start()
    {
        
        
        

        if (SaveManager.instance.CurrentLvl < 10)
        {
            for (int i = 0; i < _levelPoints.Length; i++)
            {
                _levelPoints[i].sprite = _pointSprites[0];
            }
            for (int i = 0; i < SaveManager.instance.CurrentLvl - 1; i++)
            {
                _levelPoints[i].sprite = _pointSprites[1];
            }
            for (int i = 0; i < _currentLvlMarker.Length; i++)
            {
                _currentLvlMarker[i].SetActive(false);
            }
            _currentLvlMarker[SaveManager.instance.CurrentLvl - 1].SetActive(true);
            _levelsImage[0].sprite= _levelSprites[0];
            _levelsImage[1].sprite = _levelSprites[1];
        }
        else
        {
            for (int i = 0; i < _levelPoints.Length; i++)
            {
                _levelPoints[i].sprite = _pointSprites[0];
            }
            for (int i = 0; i < SaveManager.instance.CurrentLvl - 10; i++)
            {
                _levelPoints[i].sprite = _pointSprites[1];
            }
            for (int i = 0; i < _currentLvlMarker.Length; i++)
            {
                _currentLvlMarker[i].SetActive(false);
            }
            _currentLvlMarker[SaveManager.instance.CurrentLvl - 10].SetActive(true);
            _levelsImage[0].sprite = _levelSprites[1];
            _levelsImage[1].sprite = _levelSprites[0];
        }
    }
}
