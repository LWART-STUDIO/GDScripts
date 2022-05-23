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
    [Header("��� ����")]
    public string Name;
    [Space(3)]
    [Header("�������� ������ ����")]
    public float BulletSpeed;
    [Space(3)]
    [Header("������ ����")]
    [AssetPreview]
    public GameObject BulletPrefub;

}
