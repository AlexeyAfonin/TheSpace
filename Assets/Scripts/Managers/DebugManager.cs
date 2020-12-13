using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [Header ("FPS")]
    [SerializeField] private Text fpsText;
    [SerializeField] private float updateInterval = 0.5F;
    private double lastInterval;
    private float fps;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        lastInterval = Time.realtimeSinceStartup;
    }
    
    private void Update() 
    {
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval) 
        {
            fps = 1.0f/Time.deltaTime;
            lastInterval = timeNow;
        }
        
        fpsText.text = "FPS: " + fps.ToString("f2");
    }
}
