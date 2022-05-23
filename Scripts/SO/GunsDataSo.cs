using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Guns Data", menuName = "Create Guns Data", order = 3)]
public class GunsDataSo : ScriptableObject
{
    public GunsData[] GunsData;
}
[System.Serializable]
public class GunsData
{
    
    public GunSO GunData;
    public bool IsBought;
    [AssetPreview]
    public Sprite ImageForShop;
    public float Price;
    public bool Pistol;
    public bool Riffle;
    public bool Shorgun;
    
}