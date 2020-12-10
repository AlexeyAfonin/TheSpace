using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIPanelStartPanel;
    [SerializeField] private GameObject UIPanelSelectingGame;
    [SerializeField] private GameObject UIPanelSettings;
    public void OpenUIStartGamePanel()
    {
        UIPanelStartPanel.SetActive(false);
        UIPanelSelectingGame.SetActive(true);
    }
    public void OpenSettingsPanel()
    {
        UIPanelStartPanel.SetActive(false);
        UIPanelSettings.SetActive(true);
    }
    public void Back()
    {
        if(UIPanelSelectingGame.activeSelf) 
            UIPanelSelectingGame.SetActive(false);
        else
            UIPanelSettings.SetActive(false);
        UIPanelStartPanel.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
