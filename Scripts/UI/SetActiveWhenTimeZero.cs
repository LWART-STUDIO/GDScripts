using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveWhenTimeZero : MonoBehaviour
{
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            ProjectTools.SetChildrenActive(gameObject, true);
        }
        else
        {
            ProjectTools.SetChildrenActive(gameObject, false);
        }
    }
}
