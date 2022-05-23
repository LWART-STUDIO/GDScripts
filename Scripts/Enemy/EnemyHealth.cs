using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [Header("Здоровье противника")]
    [SerializeField]private int _health=1;
    [Header("Табличка при хедшоте")]
    //[SerializeField] private GameObject _headShotText;
    [SerializeField] private bool _showHelth;
    [SerializeField] private GameObject _healthCanvas;
    [SerializeField] private GameObject _helthBarPrefub;
    private Enemy _enemy;
    private GameObject[] _healthBars;
    private int _healthPoints=4;
    private HeadShotTextMarker _headShotTextMarker;

    private void Start()
    {
        _headShotTextMarker=FindObjectOfType<HeadShotTextMarker>();
        if (!_showHelth)
        {
            _healthCanvas.SetActive(false);
        }
        else
        {
            _healthCanvas.SetActive(true);
        }
        _healthBars= new GameObject[_health];
        _enemy = GetComponent<Enemy>();
        if( _showHelth)
        {
            for(int i=0; i < _health; i++)
            {
              _healthBars[i]=Instantiate(_helthBarPrefub, _healthCanvas.transform);
            }
        }
    }

    public void GetShot(bool pisol, bool riffle, bool shotgun)
    {
        if (pisol)
        {
            _health -= 1;
            if (_showHelth)
            {
                if (_health >= 0)
                {
                    Destroy(_healthBars[_health]);
                }
                
            }
            
        }
        if (riffle)
        {
            // _healthPoints -= 3;
            _health -= 1;
            if (_showHelth)
            {
                if (_health >= 0)
                {
                    Destroy(_healthBars[_health]);
                }

            }
        }
        if (shotgun)
        {
            //_healthPoints -= 2;
            _health -= 1;
            if (_showHelth)
            {
                if (_health >= 0)
                {
                    Destroy(_healthBars[_health]);
                }

            }
        }
        
    }
    public void GetHeadShot()
    {
        ProjectTools.SetChildrenActive(_headShotTextMarker.gameObject, true);
        _enemy.KilldInHead = true;
        //_headShotText.SetActive(true);
        _healthCanvas.SetActive(false);
        _health -= 6;
        
    }
    public void GetKilled()
    {
        _health = 0;
    }
    private void Update()
    {
        if (_healthPoints <= 0)
        {
            _health -= 1;
            _healthPoints = 4;
            if (_showHelth)
            {
                if (_health >= 0)
                {
                    Destroy(_healthBars[_health]);
                }
            }
            
        }
        if (_health <= 0)
        {
            _healthCanvas.SetActive(false);
            _enemy.Kill();
            
        }
    }
}
