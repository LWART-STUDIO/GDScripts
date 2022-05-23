using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private float _jumpDepth;
    [SerializeField] private float _jumpHigh;
    [SerializeField] private float _jumpSpeed;
    public bool Jump;
    private float _yPosition;
    private bool _doDown=true;
    private bool _doUp=false;
    private bool _doMidle=false;

    private void FixedUpdate()
    {
        _yPosition=_holder.transform.localPosition.y;
        if (Jump)
        {
            if (_doDown)
            {
                if (_yPosition >= _jumpDepth)
                {
                    _yPosition -= _jumpSpeed * Time.fixedUnscaledDeltaTime;
                    
                }
                else
                {
                    _doDown = false;
                    _doUp = true;
                }
            }
            if (_doUp)
            {
                if (_yPosition <= _jumpHigh)
                {
                    _yPosition += _jumpSpeed * Time.fixedUnscaledDeltaTime;
                }
                else
                {
                    _doUp = false;
                    _yPosition = 0;
                    _doMidle = false;
                    Jump = false;
                    _doDown = true;
                }
            }
            
            
        }
        
        _holder.transform.localPosition = new Vector3(0, _yPosition, 0);
    }
}
