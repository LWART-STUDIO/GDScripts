using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndLvlTrigger : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneCountText;
    [SerializeField] private float _monyToGive;
    public float MoneyToGive => _monyToGive;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _moneyPlusText;
    [SerializeField] private Animator _weaponStore;
    [SerializeField] private GameObject _lastTrigger;
    private int _curentScene;
    private bool _levelEnded=false;
    private void Start()
    {

        _moneCountText.text = "" + SaveManager.instance.Money;
        _curentScene=SceneManager.GetActiveScene().buildIndex;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
        {
            if (!_levelEnded)
            {
                Destroy(_lastTrigger);
                _weaponStore.Play("Open");
                if (_curentScene >= 18 || _curentScene <= 0)
                {
                    SaveManager.instance.CurrentLvl = 1;
                    SaveManager.instance.Save();

                }
                else
                {
                    SaveManager.instance.CurrentLvl += 1;
                    SaveManager.instance.Save();
                }
                _moneyPlusText.text = "+" + _monyToGive;
                SaveManager.instance.Money += _monyToGive;
                SaveManager.instance.CurrentLevelCounter++;
                SaveManager.instance.Save();
                
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;
                if (SaveManager.instance.CurrentLvl == 10)
                {
                    SaveManager.instance.NewLocation = true;
                    SaveManager.instance.Save();
                }
                else if (SaveManager.instance.CurrentLvl == 1)
                {
                    SaveManager.instance.TutorialEnded = true;
                    SaveManager.instance.NewLocation = true;
                    SaveManager.instance.Save();
                }
                else
                {
                    SaveManager.instance.NewLocation = false;
                    SaveManager.instance.Save();
                }
                _endPanel.SetActive(true);
                _levelEnded = true;
            }
           

        }
    }
}
