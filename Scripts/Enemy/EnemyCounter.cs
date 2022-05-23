using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UniversalTrigger))]
public class EnemyCounter : MonoBehaviour
{
    [Header("Кол-во врагов на споте")]
    [SerializeField]private int _maxCount;
    private int _count=0;
    public int MaxCount => _maxCount;
    private UniversalTrigger _universalTrigger;
    public bool KilledInHead;
    private void Start()
    {
        _universalTrigger = GetComponent<UniversalTrigger>();
    }
    public void CountPlus()
    {
        _count++;
        if (KilledInHead)
        {
            _universalTrigger.HeadShotCounter++;
            KilledInHead = false;
        }
        
    }
    private void FixedUpdate()
    {
        if (_count >= _maxCount)
        {
            _universalTrigger.KilledAll = true;
        }
    }
}
