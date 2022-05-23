using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLvLPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneCountText;
    [SerializeField] private TMP_Text _lvlEndText;
    [SerializeField] private int _curentLvlindex;
    public int CurrentLvlIndex => _curentLvlindex;

    private void Start()
    {
        _curentLvlindex = SaveManager.instance.CurrentLevelCounter-1;
        _lvlEndText.text = "" + _curentLvlindex;
         
    }
    public void ShowShopPanel()
    {
        _moneCountText.text = "" + SaveManager.instance.Money;
        
    }
    private void Update()
    {
        _moneCountText.text = "" + SaveManager.instance.Money;
    }

}
