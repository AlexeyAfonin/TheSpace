using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSpace;

public class SpawnStarOnClickButton : MonoBehaviour
{
    private StarInformation _gameMSOALS = new StarInformation();
    [SerializeField] private int _numberStar;
    public enum TypeStar{Heavy, Light}
    [SerializeField] private TypeStar _typeSpawnSter;
    [SerializeField] private GameObject _activeStar;
    [SerializeField] private GameObject _manager;
    [SerializeField] private GameObject _otherPanel;
    [SerializeField] private GameObject _stageStarEvolutions_UIPanel;

    void Start()
    {
        _gameMSOALS = _manager.GetComponent<StarInformation>();
        _activeStar = GameObject.FindGameObjectWithTag("Star");
    }

    void Update()
    {
        _activeStar = GameObject.FindGameObjectWithTag("Star");

        Camera.main.transform.position = new Vector3(0, 0, _activeStar.transform.position.z - _activeStar.transform.localScale.z);
    }

    public void SpawnStar()
    {
        GameObject oldStar;
        
        oldStar = _activeStar;

        if(_typeSpawnSter == TypeStar.Light)
            _activeStar = Instantiate(_gameMSOALS._stageLightStars[_numberStar], new Vector3(0, 0, 0), Quaternion.identity);
        else
            _activeStar = Instantiate(_gameMSOALS._stageHeavyStars[_numberStar], new Vector3(0, 0, 0), Quaternion.identity);

        if(_activeStar.tag == "BlackHole") 
        {
            _activeStar.GetComponent<BlackHole>().enabled = false;

            _activeStar.tag = "Star";            
        }

        _activeStar.transform.localScale = oldStar.transform.localScale;
        _activeStar.GetComponent<SpaceObject>().radius = oldStar.GetComponent<SpaceObject>().radius;
        _activeStar.GetComponent<SpaceObject>().power = oldStar.GetComponent<SpaceObject>().power;
        _activeStar.GetComponent<SpaceObject>().onSelected = false;

        Destroy(oldStar);
    }

    public void Back()
    {
        this.transform.parent.gameObject.SetActive(false);
        if(_otherPanel != null) _otherPanel.SetActive(true);
    }
    public void OpenUIPanelStageStarEvolutions()
    {
        _stageStarEvolutions_UIPanel.SetActive(true);
    }
}
