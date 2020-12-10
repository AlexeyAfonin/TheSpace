using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerController : MonoBehaviour
{
    [Header ("UI панели")]
    [SerializeField] private GameObject basicGameUIPanel;
    [SerializeField] private GameObject moveGameUIPanel;
    [SerializeField] private GameObject actionGameUIPanel;
    [SerializeField] private GameObject structureGameUIPanel;

    [Header ("Right UIPanel - Главная")]
    [SerializeField] private Camera cam;
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
    [SerializeField] private GameObject spaceObjectsPanell; //панель с космическими объектами
    [SerializeField] private GameObject planets; //панель с планетами
    [SerializeField] private GameObject satellites; //панель со спутниками
    [SerializeField] private GameObject stars; //панель со звездами
    [SerializeField] private GameObject blackholes; //панель с черными дырами
    [SerializeField] private GameObject other; //панель с другими космическими объектами

    [Header("UI pause панель")]
    public GameObject uiPausePanel; //открывающаяся панель при паузе 

    private bool trackMovement = false;
    private bool _zooming = false;
    private bool _zoomIn;

    public void OpenOrClosePanel()
    {
        if(spaceObjectsPanell.activeSelf == true)
            spaceObjectsPanell.SetActive(false);
        else
            spaceObjectsPanell.SetActive(true);
    }
    public void ShowPlanets()
    {
        planets.SetActive(true);
        satellites.SetActive(false);
        stars.SetActive(false);
        blackholes.SetActive(false);
        other.SetActive(false);
    }
    public void ShowSatellites()
    {
        planets.SetActive(false);
        satellites.SetActive(true);
        stars.SetActive(false);
        blackholes.SetActive(false);
        other.SetActive(false);
    }
    public void ShowStars()
    {
        planets.SetActive(false);
        satellites.SetActive(false);
        stars.SetActive(true);
        blackholes.SetActive(false);
        other.SetActive(false);
    }
    public void ShowBlackHoles()
    {
        planets.SetActive(false);
        satellites.SetActive(false);
        stars.SetActive(false);
        blackholes.SetActive(true);
        other.SetActive(false);
    }
    public void ShowOther()
    {
        planets.SetActive(false);
        satellites.SetActive(false);
        stars.SetActive(false);
        blackholes.SetActive(false);
        other.SetActive(true);
    }

    public void Update()
    {
        if(selectSpaceObject != null)
        {
            if(trackMovement)
            {
                cam.gameObject.transform.position = new Vector3(selectSpaceObject.transform.position.x - selectSpaceObject.transform.localScale.x, selectSpaceObject.transform.localScale.y, selectSpaceObject.transform.position.z - selectSpaceObject.transform.localScale.z);
                cam.gameObject.transform.LookAt(selectSpaceObject.transform);
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
        basicGameUIPanel.SetActive(true);
        moveGameUIPanel.SetActive(false);
        actionGameUIPanel.SetActive(false);
    }

    /*Панель "Движение"*/
    public void OpenMovePanel()
    {
        basicGameUIPanel.SetActive(false);
        moveGameUIPanel.SetActive(true);
        actionGameUIPanel.SetActive(false);
    }
    public void TrackMovement() //Переключение камеры к космическому объекту
    {
        trackMovement = true;
        excretion.SetActive(false);
    }
    public void TrackSolarSystem() //Переключение камеры к солнечной системе
    {
        trackMovement = false;
        cam.gameObject.transform.position = new Vector3(20000, 40000, 0);
        cam.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
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

    IEnumerator ZoomInOut() //Изменение позиции камеры (зумирование)
    {
        while(_zooming)
        {
            if(_zoomIn)
            {
                if(cam.gameObject.transform.position != new Vector3(500, 1000, 0))
                {
                    cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x - 250f, cam.gameObject.transform.position.y - 500f, cam.gameObject.transform.position.z);
                }
            }
            else
            {
                if(cam.gameObject.transform.position != new Vector3(20000, 40000, 0))
                {
                    cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x + 250f, cam.gameObject.transform.position.y + 500f, cam.gameObject.transform.position.z);
                }
            }

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    /*Панель "Действия"*/
    public void OpenActionPanel()
    {
        basicGameUIPanel.SetActive(false);
        moveGameUIPanel.SetActive(false);
        actionGameUIPanel.SetActive(true);
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
    public void Quit(){UnityEngine.SceneManagement.SceneManager.LoadScene(0);} //Выход в главное меню
}
