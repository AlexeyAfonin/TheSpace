using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [Header ("FPS")]
    [SerializeField] private Text _fpsText;
    [SerializeField] private float _updateInterval = 0.5F;
    private double _lastInterval;
    private float _fps;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        _lastInterval = Time.realtimeSinceStartup;
    }
    
    private void Update() 
    {
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > _lastInterval + _updateInterval) 
        {
            _fps = 1.0f/Time.deltaTime;
            _lastInterval = timeNow;
        }
        
        _fpsText.text = "FPS: " + _fps.ToString("f2");
    }
}
