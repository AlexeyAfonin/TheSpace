using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ColorSpectrum : MonoBehaviour
{
    [Header ("Спектр цвета")]
    [SerializeField] private Image _point;
    private Texture2D _texture;

    [Header ("Слайдеры цвета")]
    [SerializeField] private Slider _redColor;
    [SerializeField] private Slider _greenColor;
    [SerializeField] private Slider _blueColor;

    [Header ("Количество оттенков цвета")]
    [SerializeField] private Text _howRed;
    [SerializeField] private Text _howGreen;
    [SerializeField] private Text _howBlue;

    [Header ("Выбранный цвет")]
    private Color32 _colorStar;
    [SerializeField] private RawImage _selectedColor;

    private void Start()
    {
        _texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        StartCoroutine(ReadPixelColor());
        if(_texture != null) _selectedColor.texture = _texture;


        _howRed.text = _redColor.value.ToString();
        _howGreen.text = _greenColor.value.ToString();
        _howBlue.text = _blueColor.value.ToString();

        //_colorStar = new Color32(Convert.ToByte(_redColor.value.ToString()), Convert.ToByte(_greenColor.value.ToString()), Convert.ToByte(_blueColor.value.ToString()), 255);
    }

    private IEnumerator ReadPixelColor()
    {
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        float x =  _point.transform.position.x;
        float y = _point.transform.position.y;

        // Read screen contents into the texture
        _texture.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
        _texture.Apply();
        
        _colorStar = _texture.GetPixel(0, 0);

        _redColor.value = Convert.ToInt32(_colorStar.a.ToString());
        _greenColor.value = Convert.ToInt32(_colorStar.b.ToString());
        _blueColor.value =  Convert.ToInt32(_colorStar.g.ToString());
    }
}