using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGranade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerConector>(out PlayerConector playerConector))
        {
            playerConector.GiveGranate();
            Destroy(gameObject);
        }
    }
}
