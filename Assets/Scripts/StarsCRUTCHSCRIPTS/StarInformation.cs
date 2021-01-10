using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSpace;

public class StarInformation : MonoBehaviour
{
    public List<GameObject> _stageLightStars;
    public List<GameObject> _stageHeavyStars;
    private int _lastStage;
    private GameObject _activeStar;
    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        _activeStar = GameObject.FindGameObjectWithTag("Star");
    }   
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_pauseMenu.activeSelf == false)
                _pauseMenu.SetActive(true);
            else
                _pauseMenu.SetActive(false);
        }
    }
    public void Back()
    {
        _pauseMenu.SetActive(false);
    }
    public void Exit()
    {
        GameObject.Destroy(GameObject.Find("DebugPanel"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
