using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOffer : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (SaveManager.instance.MusicTogle)
        {
            _audio.mute = false;
        }
        else
        {
            _audio.mute = true;
        }
    }
}
