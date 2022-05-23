using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHearRotation : MonoBehaviour
{
    private float _speed=60;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back, _speed * Time.fixedUnscaledDeltaTime);
    }
}
