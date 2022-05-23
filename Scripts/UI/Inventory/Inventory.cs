using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _allGuns;
    [SerializeField] private GameObject _pistols;
    [SerializeField] private GameObject _riffles;
    [SerializeField] private GameObject _shotguns;
    [Space(5f)]


    [SerializeField] private GameObject _allGunsContent;
    [SerializeField] private GameObject _pistolsContent;
    [SerializeField] private GameObject _rifflesContent;
    [SerializeField] private GameObject _shotgunsContent;
    [Space(5f)]


    [SerializeField] private GunsDataSo _gunsData;
    [SerializeField] private GameObject _panelPrefab;
    [SerializeField] private List<GameObject> _allGunsPanels;
    [SerializeField] private List<GameObject> _pistolsPanels;
    [SerializeField] private List<GameObject> _rifflesPanels;
    [SerializeField] private List<GameObject> _shotgunsPanels;



    private void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1 * 0.02f; 
        for(int i = 0; i < _gunsData.GunsData.Length; i++)
        {
            if (_gunsData.GunsData[i].IsBought)
            {
                GameObject gunPanel = Instantiate(_panelPrefab, _allGunsContent.transform);
                GunCell gunCell = gunPanel.GetComponent<GunCell>();
                gunCell.ConfigureCell(_gunsData.GunsData[i].ImageForShop, i);
                _allGunsPanels.Add(gunPanel);
                _allGuns.GetComponent<FadeInOnVisable>().CanvasGroups = _allGunsPanels;
                if (_gunsData.GunsData[i].Pistol)
                {
                    GameObject gunPanel1 = Instantiate(_panelPrefab, _pistolsContent.transform);
                    GunCell gunCell1 = gunPanel1.GetComponent<GunCell>();
                    gunCell1.ConfigureCell(_gunsData.GunsData[i].ImageForShop, i);
                    _pistolsPanels.Add(gunPanel1);
                    _pistols.GetComponent<FadeInOnVisable>().CanvasGroups = _pistolsPanels;
                }
                if (_gunsData.GunsData[i].Riffle)
                {
                    GameObject gunPanel2 = Instantiate(_panelPrefab, _rifflesContent.transform);
                    GunCell gunCell2 = gunPanel2.GetComponent<GunCell>();
                    gunCell2.ConfigureCell(_gunsData.GunsData[i].ImageForShop, i);
                    _rifflesPanels.Add(gunPanel2);
                    _riffles.GetComponent<FadeInOnVisable>().CanvasGroups = _rifflesPanels;
                }
                if (_gunsData.GunsData[i].Shorgun)
                {
                    GameObject gunPanel3 = Instantiate(_panelPrefab, _shotgunsContent.transform);
                    GunCell gunCell3 = gunPanel3.GetComponent<GunCell>();
                    gunCell3.ConfigureCell(_gunsData.GunsData[i].ImageForShop, i);
                    _shotgunsPanels.Add(gunPanel3);
                    _shotguns.GetComponent<FadeInOnVisable>().CanvasGroups = _shotgunsPanels;
                }
            }
        }
            
    }
}
