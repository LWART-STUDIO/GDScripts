using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoomText : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _boomText;
    private Tween _fadeTween;
    private bool _doTest;
    private void Start()
    {
        _boomText.SetActive(false);
    }
    private void FixedUpdate()
          {

          if (_particleSystem.IsAlive())
          {
            _doTest = false;
            StartCoroutine(StartTimer());
            _boomText.SetActive(true);
          }
          else
          {
              //_boomText.SetActive(false);
          }
      }
    IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StopCoroutine(StartTimer());
        _fadeTween = _boomText.GetComponent<Image>().DOFade(0, 0.5f).SetUpdate(true);
        _doTest = true;

    }
    private void LateUpdate()
    {

        if (_doTest)
        {
            if (!_fadeTween.IsActive())
            {
                _boomText.SetActive(false);
            }
        }
    }
}
