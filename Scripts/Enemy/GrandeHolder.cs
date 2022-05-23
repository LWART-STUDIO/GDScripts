using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandeHolder : MonoBehaviour
{
    [SerializeField] private GameObject _granade;
    public bool IsHolding;
    private void Start()
    {
        //_granade = GetComponentInParent<Granade>().gameObject;
    }
    private void FixedUpdate()
    {
        if (IsHolding)
        {
            _granade.transform.position = transform.position;
            _granade.transform.rotation= transform.rotation;
        }
    }
}
