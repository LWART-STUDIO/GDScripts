using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float MinimumValue=0;
    public float MaximumValue=100;
    public float CurrentValue;

    [SerializeField] private Image _mask;

    private void Update()
    {
        SetCurrentFill();
    }
    private void SetCurrentFill()
    {
    
        float currentOffset = CurrentValue - MinimumValue;
        float maximumOffset=MaximumValue - MinimumValue;
        float fillAmount = currentOffset / maximumOffset;
        _mask.fillAmount = fillAmount;
    }

}
