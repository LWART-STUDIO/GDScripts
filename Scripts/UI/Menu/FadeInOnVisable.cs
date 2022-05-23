using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[DefaultExecutionOrder(3)]
public class FadeInOnVisable : MonoBehaviour
{
    public List<GameObject> CanvasGroups;
    
    private void OnEnable()
    {
        Time.timeScale = 1;
        for (int i = 0; i < CanvasGroups.Count; i++)
        {
            CanvasGroups[i].gameObject.SetActive(false);
        }
        StartCoroutine(ShowObjects());
        
    }
    private IEnumerator ShowObjects()
    {
        
        foreach (GameObject canvasGroup in CanvasGroups)
        {
            Debug.Log(Time.timeScale);
            Debug.Log(CanvasGroups.Count);
            Debug.Log(canvasGroup);
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.GetComponent<CanvasGroup>().DOFade(0, 0.3f).From();
            canvasGroup.GetComponentInChildren<Child>().transform.DOPunchPosition(new Vector3(0, 20f, 0), 0.4f, 5,1);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
