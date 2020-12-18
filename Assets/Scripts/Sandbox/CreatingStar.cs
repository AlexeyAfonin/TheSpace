using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CreatingStar : MonoBehaviour
{
    public VisualEffect star;
    [SerializeField] private Color _color;
    [SerializeField] private Gradient _gradient;
    private GradientColorKey[] _gradientColorKey = new GradientColorKey[2];
    private GradientAlphaKey[] _gradientAlphaKey = new GradientAlphaKey[2];

    private void Update()
    {
        _gradientColorKey[0].color = _color;
        _gradientAlphaKey[0].time = 0.0f;
        _gradientAlphaKey[0].alpha = 1.0f;
        _gradientColorKey[1].color = _color;
        _gradientAlphaKey[1].time = 1.0f;
        _gradientAlphaKey[1].alpha = 1.0f;
        _gradient.SetKeys(_gradientColorKey, _gradientAlphaKey);
        
        star.SetGradient("FlameColor",_gradient);
    }

    /*
    private void Update()
    {
        _gradientColorKey[0].color = _color;
        _gradientAlphaKey[0].time = 0.0f;

        _gradientColorKey[1].color = _color;
        _gradientAlphaKey[1].time = 0.75f;

        _gradientColorKey[2].color = _color;
        _gradientAlphaKey[2].time = 1.0f;

        _gradientColorKey[3].color = _color;
        _gradientAlphaKey[3].time = 0.75f;

        _gradientColorKey[4].color = _color;
        _gradientAlphaKey[4].time = 0.0f;

        _gradient.SetKeys(_gradientColorKey, _gradientAlphaKey);

        star.SetGradient("FlameColor",_gradient);
    }
    */
}
