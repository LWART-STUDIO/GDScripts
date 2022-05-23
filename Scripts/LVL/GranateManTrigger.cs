using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateManTrigger : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    [SerializeField] private PlayerConector _conector;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerConector>(out PlayerConector playerConector))
        {
            _conector = playerConector;
            _enemyAnimationController.PlayAnimation();
        }
    }
}
