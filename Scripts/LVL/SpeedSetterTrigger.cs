using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSetterTrigger : MonoBehaviour
{
    [SerializeField] private float _newSpeed=2;
    private float _beginSpeed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
        {
            _beginSpeed = playerMover.Speed;
            playerMover.SetSpeed(_newSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
        {
            playerMover.SetSpeed(_beginSpeed);
        }
    }


}
