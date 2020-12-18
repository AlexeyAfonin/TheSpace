using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _UIPanelStartPanel;
    [SerializeField] private GameObject _UIPanelSelectingGame;
    [SerializeField] private GameObject _UIPanelSettings;

    public void OpenUIStartGamePanel()
    {
        _UIPanelStartPanel.SetActive(false);
        _UIPanelSelectingGame.SetActive(true);
    }
    public void OpenSettingsPanel()
    {
        _UIPanelStartPanel.SetActive(false);
        _UIPanelSettings.SetActive(true);
    }
    public void Back()
    {
        if(_UIPanelSelectingGame.activeSelf) 
            _UIPanelSelectingGame.SetActive(false);
        else
            _UIPanelSettings.SetActive(false);
        _UIPanelStartPanel.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
