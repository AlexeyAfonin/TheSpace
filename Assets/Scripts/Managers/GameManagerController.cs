using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSpace;
using System.IO;

public class GameManagerController : MonoBehaviour
{
    [HideInInspector] public GameObject selectedObject;
    private GUIManagerController guiMC;
    private KeyCode Exit = KeyCode.Escape;

    private void Start()
    {
        guiMC = GameObject.FindWithTag("Manager").GetComponent<GUIManagerController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) //Выбор космического объекта с помощь нажатия ЛКМ
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.tag == "Star")||(hit.transform.tag == "Planet"))
                {
                    selectedObject = hit.transform.gameObject;
                    selectedObject.GetComponent<SpaceObject>().onSelected = true;
                }
            }
        }
        if(Input.GetKeyDown(Exit)) //Открытие панели "меню"
        {
            if(guiMC.uiPausePanel.activeSelf) 
                guiMC.uiPausePanel.SetActive(false);
            else 
                guiMC.uiPausePanel.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Keypad0)) Time.timeScale = 0;
        if(Input.GetKeyDown(KeyCode.Keypad1)) Time.timeScale = 1;
        if(Input.GetKeyDown(KeyCode.Keypad2)) Time.timeScale = 4;
        if(Input.GetKeyDown(KeyCode.Keypad3)) Time.timeScale = 6;
        if(Input.GetKeyDown(KeyCode.Keypad4)) Time.timeScale = 8;
        if(Input.GetKeyDown(KeyCode.Keypad5)) Time.timeScale = 10;
    }
}
