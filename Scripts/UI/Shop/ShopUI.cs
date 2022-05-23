using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Coffee.UIExtensions;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GunsDataSo _gunsData;
    [SerializeField] private Image[] _shopIconPoints = new Image[3] { null, null, null };
    [SerializeField] private TMP_Text[] _prises = new TMP_Text[3] { null, null, null };
    private int _count = 0;
    private int _maxGun = 0;
    private int[] _index = new int[3] { -1, -1, -1 };
    [SerializeField] private GameObject[] _panels = new GameObject[3] { null, null, null };
    [SerializeField] private GameObject[] _buttons = new GameObject[3] { null, null, null };
    [SerializeField] private UIParticle[] _particleSystem = new UIParticle[3];
    

    private void Start()
    {
        
        for (int i = 0; i < _gunsData.GunsData.Length; i++)
        {
            if (_index[0] == -1)
            {
                if (!_gunsData.GunsData[i].IsBought)
                {
                    _panels[0].SetActive(true);
                    _shopIconPoints[0].sprite = _gunsData.GunsData[i].ImageForShop;
                    Tween tween= _shopIconPoints[0].transform.DOLocalMoveY(35f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Flash);
                    _prises[0].text = _gunsData.GunsData[i].Price.ToString();
                    _index[0] = i;
                }
                else
                {
                    _panels[0].SetActive(false);
                }
            }
            if (_index[1] == -1)
            {
                if (!_gunsData.GunsData[i].IsBought && _index[0] != i)
                {
                    _panels[1].SetActive(true);
                    _shopIconPoints[1].sprite = _gunsData.GunsData[i].ImageForShop;
                    Tween tween = _shopIconPoints[1].transform.DOLocalMoveY(35f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Flash);
                    _prises[1].text = _gunsData.GunsData[i].Price.ToString();
                    _index[1] = i;
                }
                else
                {
                    _panels[1].SetActive(false);
                }
            }

        }
        for (int i = _gunsData.GunsData.Length - 1; i > 0; i--)
        {

            if (!_gunsData.GunsData[i].IsBought && _index[0] != i && _index[1] != i)
            {
                if (_count != 0)
                {
                    _maxGun = Random.Range(i, _count);
                    if (!_gunsData.GunsData[_maxGun].IsBought)
                    {
                        _panels[2].SetActive(true);
                        _shopIconPoints[2].sprite = _gunsData.GunsData[_maxGun].ImageForShop;
                        Tween tween = _shopIconPoints[2].transform.DOLocalMoveY(35f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Flash);
                        _prises[2].text = _gunsData.GunsData[_maxGun].Price.ToString();
                        _index[2] = _maxGun;
                    }
                    else
                    {
                        _panels[2].SetActive(true);
                        _shopIconPoints[2].sprite = _gunsData.GunsData[i].ImageForShop;
                        Tween tween = _shopIconPoints[2].transform.DOLocalMoveY(35f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Flash);
                        _prises[2].text = _gunsData.GunsData[i].Price.ToString();
                        _index[2] = i;
                    }
                    
                }
                else
                {
                    if (_index[2] == -1)
                    {
                        _count = i;
                        _panels[2].SetActive(true);
                        _shopIconPoints[2].sprite = _gunsData.GunsData[i].ImageForShop;
                        Tween tween = _shopIconPoints[2].transform.DOLocalMoveY(35f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Flash);
                        _prises[2].text = _gunsData.GunsData[i].Price.ToString();
                        _index[2] = i;
                    }
                }

            }
            else if (_count == 0)
            {
                _panels[2].SetActive(false);
            }

        }

    }
    public void BuyOne()
    {
        if (SaveManager.instance.Money >= _gunsData.GunsData[_index[0]].Price)
        {
            SaveManager.instance.Money -= _gunsData.GunsData[_index[0]].Price;
            SaveManager.instance.BoughtGuns[_index[0]] = true;
            _gunsData.GunsData[_index[0]].IsBought = true;
            SaveManager.instance.CurrentGun = _index[0];
            SaveManager.instance.Save();
            _buttons[0].SetActive(false);
            Tween mytween= _shopIconPoints[0].gameObject.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.9f, 4, 1f).SetUpdate(true);
           // _panels[0].GetComponent<Image>().color = new Color(0.4764151f, 0.7156867f, 1f);
            _particleSystem[0].Play();
            

        }


    }
    public void BuyTwo()
    {
        if (SaveManager.instance.Money >= _gunsData.GunsData[_index[1]].Price)
        {
            SaveManager.instance.Money -= _gunsData.GunsData[_index[1]].Price;
            SaveManager.instance.BoughtGuns[_index[1]] = true;
            _gunsData.GunsData[_index[1]].IsBought = true;
            SaveManager.instance.CurrentGun = _index[1];
            SaveManager.instance.Save();
            _buttons[1].SetActive(false);
            Tween mytween = _shopIconPoints[1].gameObject.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.9f, 4, 1f).SetUpdate(true);
           // _panels[1].GetComponent<Image>().color = new Color(0.4764151f, 0.7156867f, 1f);
            _particleSystem[1].Play();
        }

    }
    public void BuyThree()
    {
        if (SaveManager.instance.Money >= _gunsData.GunsData[_index[2]].Price)
        {
            SaveManager.instance.Money -= _gunsData.GunsData[_index[2]].Price;
            SaveManager.instance.BoughtGuns[_index[2]] = true;
            _gunsData.GunsData[_index[2]].IsBought = true;
            SaveManager.instance.CurrentGun = _index[2];
            SaveManager.instance.Save();
            _buttons[2].SetActive(false);
            Tween mytween = _shopIconPoints[2].gameObject.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.9f, 4, 1f).SetUpdate(true);
            //_panels[2].GetComponent<Image>().color = new Color(0.4764151f, 0.7156867f, 1f);
            _particleSystem[2].Play();
        }

    }

}

