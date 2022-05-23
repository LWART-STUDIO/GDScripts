using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HeadShotText : MonoBehaviour
{
    private Tween _fadeTween;
    private bool _isFinished;
    private bool _doTest;
    private void OnEnable()
    {
        _doTest = false;
        _isFinished = false;
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StopCoroutine(StartTimer());
       _fadeTween= gameObject.GetComponent<Image>().DOFade(0,0.5f).SetUpdate(true);
        _doTest = true;

    }
    private void LateUpdate()
    {

        if (_doTest)
        {
            if (!_fadeTween.IsActive())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
