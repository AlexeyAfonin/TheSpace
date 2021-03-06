﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerController : MonoBehaviour
{
    [Header ("UI панели")]
    [SerializeField] private GameObject _basicGameUIPanel;
    [SerializeField] private GameObject _moveGameUIPanel;
    [SerializeField] private GameObject _actionGameUIPanel;
    [SerializeField] private GameObject _structureGameUIPanel;

    [Header ("Right UIPanel - Главная")]
    [SerializeField] private Camera _cam;
    [HideInInspector] public GameObject selectSpaceObject;
    public GameObject excretion;
    public Text type;
    public Text nameObj;
    public Text age;
    public Text mass;
    public Text radius;
    public Text density;
    public Text surfaceTemperature;
    public Text totalVelocity;
    public Text rotationalPeriodAroundAxis;
    public Text rotationalPeriodAroundSun;
    public Text surfaceGravity;
    public Text escapeVelocity;
       
    [Header("UI Панель с космическими оъектами")]
    [SerializeField] private GameObject _spaceObjectsPanel; //панель с космическими объектами
    [SerializeField] private GameObject _planets; //панель с планетами
    [SerializeField] private GameObject _satellites; //панель со спутниками
    [SerializeField] private GameObject _stars; //панель со звездами
    [SerializeField] private GameObject _blackholes; //панель с черными дырами
    [SerializeField] private GameObject _other; //панель с другими космическими объектами

    [Header("UI pause панель")]
    public GameObject uiPausePanel; //открывающаяся панель при паузе 

    private bool _trackMovement = false;
    private bool _zooming = false;
    private bool _zoomIn;

    public void OpenOrClosePanel()
    {
        if(_spaceObjectsPanel.activeSelf == true)
            _spaceObjectsPanel.SetActive(false);
        else
            _spaceObjectsPanel.SetActive(true);
    }
    public void ShowPlanets()
    {
        _planets.SetActive(true);
        _satellites.SetActive(false);
        _stars.SetActive(false);
        _blackholes.SetActive(false);
        _other.SetActive(false);
    }
    public void ShowSatellites()
    {
        _planets.SetActive(false);
        _satellites.SetActive(true);
        _stars.SetActive(false);
        _blackholes.SetActive(false);
        _other.SetActive(false);
    }
    public void ShowStars()
    {
        _planets.SetActive(false);
        _satellites.SetActive(false);
        _stars.SetActive(true);
        _blackholes.SetActive(false);
        _other.SetActive(false);
    }
    public void ShowBlackHoles()
    {
        _planets.SetActive(false);
        _satellites.SetActive(false);
        _stars.SetActive(false);
        _blackholes.SetActive(true);
        _other.SetActive(false);
    }
    public void ShowOther()
    {
        _planets.SetActive(false);
        _satellites.SetActive(false);
        _stars.SetActive(false);
        _blackholes.SetActive(false);
        _other.SetActive(true);
    }

    public void Update()
    {
        if(selectSpaceObject != null)
        {
            if(_trackMovement)
            {
                _cam.gameObject.transform.position = new Vector3(selectSpaceObject.transform.position.x - selectSpaceObject.transform.localScale.x, selectSpaceObject.transform.localScale.y, selectSpaceObject.transform.position.z - selectSpaceObject.transform.localScale.z);
                _cam.gameObject.transform.LookAt(new Vector3(selectSpaceObject.transform.position.x, selectSpaceObject.transform.position.y, selectSpaceObject.transform.position.z));
                excretion.SetActive(false);
            }
            else
            {
                excretion.SetActive(true);
                excretion.transform.localScale = new Vector3(500, 1, 500);
                excretion.transform.position = new Vector3(selectSpaceObject.transform.position.x, selectSpaceObject.transform.position.y, selectSpaceObject.transform.position.z);
            }

            mass.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass.ToString() + " * 10²¹ кг";
            radius.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius.ToString() + " км";
            density.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density.ToString() + " г/см³";
            surfaceTemperature.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature.ToString() + " °С";
            
            totalVelocity.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().totalVelocity.ToString() + " км/сек";
            surfaceGravity.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceGravity.ToString() + " м/сек²";
            escapeVelocity.text = selectSpaceObject.GetComponent<TheSpace.SpaceObject>().escapeVelocity.ToString() + " км/сек";
        }
    }

    //Действия над космическим объектом
    /*Панель "Главная"*/
    public void OpenBasicPanel()
    {
        _basicGameUIPanel.SetActive(true);
        _moveGameUIPanel.SetActive(false);
        _actionGameUIPanel.SetActive(false);
    }

    /*Панель "Движение"*/
    public void OpenMovePanel()
    {
        _basicGameUIPanel.SetActive(false);
        _moveGameUIPanel.SetActive(true);
        _actionGameUIPanel.SetActive(false);
    }
    public void TrackMovement() //Переключение камеры к космическому объекту
    {
        _trackMovement = true;
        excretion.SetActive(false);
    }
    public void TrackSolarSystem() //Переключение камеры к солнечной системе
    {
        _trackMovement = false;
        _cam.gameObject.transform.position = new Vector3(20000, 40000, 0);
        _cam.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        if(selectSpaceObject == null) excretion.SetActive(false);
    }

    //Зумирование
    //Прибоизить камеру
    public void ZoomIn_ButtonDown() 
    {
        _zooming = true;
        _zoomIn = true;
        StartCoroutine(ZoomInOut());
    }
    public void ZoomIn_ButtonUp()
    {
        _zooming = false;
        StopCoroutine(ZoomInOut());
    }
    //Отдалить камеру
    public void ZoomOut_ButtonDown()
    {
        _zoomIn = false;
        _zooming = true;
        StartCoroutine(ZoomInOut());
    }
    public void ZoomOut_ButtonUp()
    {
        _zooming = false;
        StopCoroutine(ZoomInOut());
    }

    private IEnumerator ZoomInOut() //Изменение позиции камеры (зумирование)
    {
        while(_zooming)
        {
            if(_zoomIn)
            {
                if(_cam.gameObject.transform.position != new Vector3(500, 1000, 0))
                {
                    _cam.gameObject.transform.position = new Vector3(_cam.gameObject.transform.position.x - 250f, _cam.gameObject.transform.position.y - 500f, _cam.gameObject.transform.position.z);
                }
            }
            else
            {
                if(_cam.gameObject.transform.position != new Vector3(20000, 40000, 0))
                {
                    _cam.gameObject.transform.position = new Vector3(_cam.gameObject.transform.position.x + 250f, _cam.gameObject.transform.position.y + 500f, _cam.gameObject.transform.position.z);
                }
            }

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    /*Панель "Действия"*/
    public void OpenActionPanel()
    {
        _basicGameUIPanel.SetActive(false);
        _moveGameUIPanel.SetActive(false);
        _actionGameUIPanel.SetActive(true);
    }
    public void BlowUp() //Взорвать космический объект
    {
        if(selectSpaceObject != null)
            selectSpaceObject.GetComponent<TheSpace.SpaceObject>().exploded = true;
    }
    public void TransitionToA_Next_StageOfLife() //Переход на следующий этап жизни Звезды
    {
        if(selectSpaceObject != null)
            selectSpaceObject.GetComponent<TheSpace.SpaceObject>().nextStage = true;
    }
    public void TransitionToA_Back_StageOfLife() //Переход на предыдущий этап жизни Звезды
    {
        if(selectSpaceObject != null)
            selectSpaceObject.GetComponent<TheSpace.SpaceObject>().backStage = true;
    }

    /*Панель "Ировое меню"*/
    public void Back(){uiPausePanel.SetActive(false);} //Скрытие pause панели
    public void Quit()
    {
        GameObject.Destroy(GameObject.Find("DebugPanel"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    } //Выход в главное меню
}
