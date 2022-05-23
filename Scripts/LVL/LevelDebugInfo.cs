using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelDebugInfo : MonoBehaviour
{
    private TMP_Text _text;
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _text.text =
            "WantRocket: " + LevelInformator.instance.WantToGiveRocketLauncher +
            " GiveRocket: " + LevelInformator.instance.GiveRocketLauncher;
    }
}
