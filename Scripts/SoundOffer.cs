using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOffer : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (SaveManager.instance.SoundTogle)
        {
            _audio.mute = false;
        }
        else
        {
            _audio.mute = true;
        }
    }
}
