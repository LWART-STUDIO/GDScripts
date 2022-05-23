using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyIKLookAtPlayer : MonoBehaviour
{
    [SerializeField] private Rig _bodyAim;
    [Range(0f, 1f)]
    public float BodyAimWeight;
    [SerializeField] private Rig _rightHandAim;
    [Range(0f, 1f)]
    public float RightHandAimWeight;
    [SerializeField] private Rig _leftHandAim;
    [Range(0f, 1f)]
    public float LeftHandAimWeight;

    private void Update()
    {
        if (_bodyAim.weight != BodyAimWeight)
        {
            _bodyAim.weight = BodyAimWeight;
        }
        if(_leftHandAim.weight != LeftHandAimWeight)
        {
            _leftHandAim.weight = LeftHandAimWeight;
        }
        if(_rightHandAim.weight != RightHandAimWeight)
        {
            _rightHandAim.weight= RightHandAimWeight;
        }
    }





}
