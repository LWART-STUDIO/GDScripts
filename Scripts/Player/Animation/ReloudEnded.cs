using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloudEnded : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
    }
    private void Start()
    {
        GetComponent<Animator>().enabled = true;
    }
    private bool _reloded=true;
    public bool Reloded => _reloded;
    public void EndAnimation()
    {
        _reloded = true;
    }
    public void StartAnimation()
    {
        _reloded=false;
    }
}
