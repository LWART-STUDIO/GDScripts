using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLocationButton : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button= GetComponent<Button>();
        _button.interactable = false;
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3f);
        _button.interactable = true;
    }
        
}
