using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Sprite[] _musicSwitcherSprites;
    [SerializeField] private Sprite[] _audioSwitcherSprites;
    [SerializeField] private Image _musicSwitcher;
    [SerializeField] private Image _audioSwitcher;

    private void Update()
    {
        if (SaveManager.instance.MusicTogle)
        {
            _musicSwitcher.sprite = _musicSwitcherSprites[0];
        }
        else
        {
            _musicSwitcher.sprite = _musicSwitcherSprites[1];
        }
        if (SaveManager.instance.SoundTogle)
        {
            _audioSwitcher.sprite = _audioSwitcherSprites[0];
        }
        else
        {
            _audioSwitcher.sprite= _audioSwitcherSprites[1];
        }
    }
    public void MusicSwith()
    {
        SaveManager.instance.MusicTogle = !SaveManager.instance.MusicTogle;
        SaveManager.instance.Save();
    }
    public void AudioSwith()
    {
        SaveManager.instance.SoundTogle=!SaveManager.instance.SoundTogle;
        SaveManager.instance.Save();

    }
}
