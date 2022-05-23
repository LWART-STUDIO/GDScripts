using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        _image.sprite = _sprites[Random.Range(0,_sprites.Length)];
    }
}
