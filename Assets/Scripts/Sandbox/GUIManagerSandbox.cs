using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIManagerSandbox : MonoBehaviour
{
    [Header ("Цвет")]
    [SerializeField] private Slider _redColor;
    [SerializeField] private Slider _greenColor;
    [SerializeField] private Slider _blueColor;

    [SerializeField] private Text _howRed;
    [SerializeField] private Text _howGreen;
    [SerializeField] private Text _howBlue;

    [SerializeField] private Image _thisColor;
    public Color32 colorStar;

    void Update()
    {
        _howRed.text = _redColor.value.ToString();
        _howGreen.text = _greenColor.value.ToString();
        _howBlue.text = _blueColor.value.ToString();

        colorStar = new Color32(Convert.ToByte(_redColor.value.ToString()), Convert.ToByte(_greenColor.value.ToString()), Convert.ToByte(_blueColor.value.ToString()), 255);

        _thisColor.color = colorStar;
    }
}
