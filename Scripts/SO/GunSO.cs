using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gun Data", menuName = "Guns/Create Gun Data", order = 2)]
public class GunSO : ScriptableObject
{
    public Gun Gun;
}

[System.Serializable]
public class Gun
{
    [Header("»Ãﬂ Œ–”∆»ﬂ")]
    public string Name;
    [Space(3)]
    [Header("œ–≈‘¿¡ Œ–”∆»ﬂ")]
    [AssetPreview]
    public GameObject Prefub;
    [Space(3)]
    [Header("¡‡Á‡ ÔÛÎ¸")]
    public BulletSo BulletsData;
    public bool OneHand;
    public int NumberOfBullets;

}
