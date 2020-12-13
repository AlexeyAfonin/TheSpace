using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [Header ("Звуки")]
    public float gameVolume;
    public float musicVolume;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        gameVolume = 50;
        musicVolume = 50;
    }

}
