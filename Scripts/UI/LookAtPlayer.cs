using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform);
    }
}
