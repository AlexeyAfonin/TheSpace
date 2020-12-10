using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header ("Музыка")]
    [SerializeField] private List<AudioSource> gameSounds;
    [SerializeField] private Slider volumeGameSlider;
    [SerializeField] private float gameVolume;

    [Header ("Игровые звуки")]
    [SerializeField] private List<AudioSource> musicSounds;
    [SerializeField] private Slider volumeMusicSlider;
    [SerializeField] private float musicVolume;
    
    private GameObject gSettings;

    void Start()
    {
        gSettings = GameObject.Find("GlobalSettingsManager");

        volumeMusicSlider.value = gSettings.GetComponent<GlobalSettings>().musicVolume;
        volumeGameSlider.value = gSettings.GetComponent<GlobalSettings>().gameVolume;
    }

    void Update()
    {
        musicVolume = volumeMusicSlider.value;
        gSettings.GetComponent<GlobalSettings>().musicVolume = musicVolume;
        gameVolume = volumeGameSlider.value;
        gSettings.GetComponent<GlobalSettings>().gameVolume = gameVolume;
        
        foreach(var sound in musicSounds)
            sound.volume = musicVolume / 100;

        foreach(var sound in gameSounds)
            sound.volume = gameVolume / 100;
    }
}
