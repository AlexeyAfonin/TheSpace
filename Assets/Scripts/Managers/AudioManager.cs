using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header ("Музыка")]
    [SerializeField] private List<AudioSource> _gameSounds;
    [SerializeField] private Slider _volumeGameSlider;
    [SerializeField] private float _gameVolume;

    [Header ("Игровые звуки")]
    [SerializeField] private List<AudioSource> _musicSounds;
    [SerializeField] private Slider _volumeMusicSlider;
    [SerializeField] private float _musicVolume;
    
    private GameObject _globalSettings;

    private void Start()
    {
        _globalSettings = GameObject.Find("GlobalSettingsManager");

        _volumeMusicSlider.value = _globalSettings.GetComponent<GlobalSettings>().musicVolume;
        _volumeGameSlider.value = _globalSettings.GetComponent<GlobalSettings>().gameVolume;
    }

    private void Update()
    {
        _musicVolume = _volumeMusicSlider.value;
        _globalSettings.GetComponent<GlobalSettings>().musicVolume = _musicVolume;
        _gameVolume = _volumeGameSlider.value;
        _globalSettings.GetComponent<GlobalSettings>().gameVolume = _gameVolume;
        
        foreach(var sound in _musicSounds)
            sound.volume = _musicVolume / 100;

        foreach(var sound in _gameSounds)
            sound.volume = _gameVolume / 100;
    }
}
