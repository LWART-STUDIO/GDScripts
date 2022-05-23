using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bullets Data", menuName ="Bullets/Create Bullets Data",order =1)]
public class BulletSo : ScriptableObject
{
    public Bullet[] bullets;
}
[System.Serializable]
public class Bullet
{
    [Header("ИМЯ ПУЛИ")]
    public string Name;
    [Space(3)]
    [Header("Скорость полета пули")]
    public float BulletSpeed;
    [Space(3)]
    [Header("Префаб пули")]
    [AssetPreview]
    public GameObject BulletPrefub;

}
