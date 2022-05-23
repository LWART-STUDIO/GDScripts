using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrow : MonoBehaviour
{
    [SerializeField] private Granade _granade;
    public bool Throw=false;
    [SerializeField] EnemyAnimationController _enemyAnimationController;
    private GrandeHolder _grandeHolder;
   [SerializeField] private GameObject _glowEffect;
    private void Start()
    {
        _grandeHolder=GetComponentInChildren<GrandeHolder>();
        _granade=GetComponentInChildren<Granade>();
        _enemyAnimationController=GetComponentInChildren<EnemyAnimationController>();
    }
    private void FixedUpdate()
    {
        if (Throw)
        {

            StartCoroutine(ThrowTimer());
            Throw = false;
        }
    }
    IEnumerator ThrowTimer()
    {
        yield return new WaitForSeconds(0.5f);
        _grandeHolder.IsHolding = false;
        _granade.Therow(Camera.main.transform.position+new Vector3(0,1f,0));
        _glowEffect.SetActive(true);
    }
}
