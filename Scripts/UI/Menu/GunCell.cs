using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using DG.Tweening;

public class GunCell : MonoBehaviour, ICell
{
    public Image Image;
    public bool Selected = false;
    [SerializeField] private int _index;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Image _panaleImage;
    [SerializeField] private Sprite _unselectedSprite;
    [SerializeField] private Sprite _selectedSprite;
    private CanvasGroup _canvasGroup;


    
    public void ConfigureCell(Sprite sprite,int index)
    {
        Image.sprite = sprite;
        _index = index;
        if (Selected)
        {
            _button.SetActive(false);
        }
    }
    private void Update()
    {

        if (SaveManager.instance.CurrentGun != _index)
        {
            _panaleImage.sprite = _unselectedSprite;
            _mainPanel.transform.DOScale(1f, 0.2f);
            _button.SetActive(true);
            Selected = false;
        }
        else
        {
            _panaleImage.sprite = _selectedSprite;
            _mainPanel.transform.DOScale(1.1f, 0.2f);
            _button.SetActive(false);
            Selected = true;
        }


    }
    public void SelectGun()
    {
        
        Selected = true;
        SaveManager.instance.CurrentGun = _index;
        SaveManager.instance.Save();
    }
}
