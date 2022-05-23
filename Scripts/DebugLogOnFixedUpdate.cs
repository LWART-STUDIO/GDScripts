using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogOnFixedUpdate : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log(Time.timeScale);
    }
}
