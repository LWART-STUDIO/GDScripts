using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsObject : MonoBehaviour
{
    public static AnalyticsObject instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
