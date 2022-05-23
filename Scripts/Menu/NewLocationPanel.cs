using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLocationPanel : MonoBehaviour
{
    [SerializeField] private GameObject _newLocationPanel;
    [SerializeField] private Sprite[] _locationSprites;
    [SerializeField] private Image[] _locationImages;

    private void Start()
    {
        if (SaveManager.instance.NewLocation)
        {
            _newLocationPanel.SetActive(true);
        }
        if (SaveManager.instance.CurrentLvl == 10)
        {
            _locationImages[0].sprite=_locationSprites[0];
            _locationImages[1].sprite = _locationSprites[1];
        }
        else
        {
            _locationImages[0].sprite = _locationSprites[1];
            _locationImages[1].sprite = _locationSprites[0];
        }
    }
    public void ExitFromPanel()
    {
        SaveManager.instance.NewLocation = false;
        SaveManager.instance.Save();
    }
}
