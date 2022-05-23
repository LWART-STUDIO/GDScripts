using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCounter : MonoBehaviour
{
    [SerializeField] private GameObject _imageHolder;
    [SerializeField] private Sprite[] _countImage = new Sprite[2] { null, null };
    [SerializeField] private GameObject _imagePrefab;
    [SerializeField] private int _triggerCount;
    [SerializeField] private int _curentTriggerDone=-1;

    private void Start()
    {
        _triggerCount=FindObjectsOfType<UniversalTrigger>().Length;
        for(int i = 0; i < _triggerCount; i++)
        {
            GameObject image = Instantiate(_imagePrefab, _imageHolder.transform);
            image.GetComponent<Image>().sprite=_countImage[0];
        }
    }
    public void CounterPlus()
    {
        _curentTriggerDone++;
        Image[] images=_imageHolder.GetComponentsInChildren<Image>();
        images[_curentTriggerDone].GetComponent<Image>().sprite = _countImage[1];
    }

}
