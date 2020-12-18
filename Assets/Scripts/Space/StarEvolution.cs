using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSpace;
using System.IO;

public class StarEvolution : MonoBehaviour
{
    [HideInInspector] public GameObject selectedObject;
    private GameObject _newStar;
    private GameObject _lastStageStar;

    [Header("Этапы жизни звезд")]
    public GameObject[] stageOfAStartLife_For_LightStar; //Для Легкой звезды
    public GameObject[] stageOfAStartLife_For_HeavyStar; //Для Тяжелой звезды
    
    private void Update()
    {
        if((selectedObject != null)&&(selectedObject.GetComponent<TheSpace.SpaceObject>().typeThisObject == SpaceObject.TypeObject.Star))
        {
            if((selectedObject.GetComponent<TheSpace.SpaceObject>().nextStage)&&(selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar < stageOfAStartLife_For_LightStar.Length)) //Переход на новую стадию жизни звезды
            {
                TransitionToANextStageOfLife(selectedObject.GetComponent<TheSpace.SpaceObject>().typeThisStar);
            }
            if((selectedObject.GetComponent<TheSpace.SpaceObject>().backStage)&&(selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar > 0)) //Переход на предыдущую стадию жизни звезды
            {
                TransitionToABackStageOfLife(selectedObject.GetComponent<TheSpace.SpaceObject>().typeThisStar);  
            }
            if(selectedObject.GetComponent<TheSpace.SpaceObject>().exploded) //Взрыв звезды
            {
                BlowUpStar();
            } 
        }
    }
    private void TransitionToANextStageOfLife(SpaceObject.TypeStar type) //Переход на новую стадию жизни звезды
    {
        selectedObject.GetComponent<TheSpace.SpaceObject>().nextStage = false;
        selectedObject.SetActive(false);
        if(type == SpaceObject.TypeStar.Light)
        {
            if(selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar < stageOfAStartLife_For_LightStar.Length)
            {
                _newStar = Instantiate(stageOfAStartLife_For_LightStar[++selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar], new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z), Quaternion.identity);
                _newStar.GetComponent<TheSpace.SpaceObject>().onSelected = true;
                _newStar.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar = selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar;
                --selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar;
                _lastStageStar = selectedObject;
                if(_newStar.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar == 1) 
                {
                    //Присваивание значений новой звезде
                    _newStar.GetComponent<TheSpace.SpaceObject>().nameObj = selectedObject.GetComponent<TheSpace.SpaceObject>().nameObj + " - Красный гигант";
                    _newStar.GetComponent<TheSpace.SpaceObject>().age = selectedObject.GetComponent<TheSpace.SpaceObject>().age;
                    _newStar.GetComponent<TheSpace.SpaceObject>().mass = selectedObject.GetComponent<TheSpace.SpaceObject>().mass / 200;
                    _newStar.GetComponent<TheSpace.SpaceObject>().radius = selectedObject.GetComponent<TheSpace.SpaceObject>().radius * 0.035f;
                    _newStar.GetComponent<TheSpace.SpaceObject>().density = selectedObject.GetComponent<TheSpace.SpaceObject>().density;
                    _newStar.GetComponent<TheSpace.SpaceObject>().surfaceTemperature = selectedObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature * 100;
                    _newStar.GetComponent<TheSpace.SpaceObject>().totalVelocity = selectedObject.GetComponent<TheSpace.SpaceObject>().totalVelocity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis = selectedObject.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis;
                    _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun = selectedObject.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun;
                    _newStar.GetComponent<TheSpace.SpaceObject>().surfaceGravity = selectedObject.GetComponent<TheSpace.SpaceObject>().surfaceGravity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().escapeVelocity = selectedObject.GetComponent<TheSpace.SpaceObject>().escapeVelocity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().power = 10;
                } 
                else if(_newStar.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar == 2) 
                {
                    //Присваивание значений новой звезде
                    _newStar.GetComponent<TheSpace.SpaceObject>().nameObj = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().nameObj + " - Белый карлик";
                    _newStar.GetComponent<TheSpace.SpaceObject>().age = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().age;
                    _newStar.GetComponent<TheSpace.SpaceObject>().mass = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().mass;
                    _newStar.GetComponent<TheSpace.SpaceObject>().radius = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().radius / 100;
                    _newStar.GetComponent<TheSpace.SpaceObject>().density = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().density;
                    _newStar.GetComponent<TheSpace.SpaceObject>().surfaceTemperature = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().surfaceTemperature;
                    _newStar.GetComponent<TheSpace.SpaceObject>().totalVelocity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().totalVelocity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis;
                    _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun;
                    _newStar.GetComponent<TheSpace.SpaceObject>().surfaceGravity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().surfaceGravity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().escapeVelocity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().escapeVelocity;
                    _newStar.GetComponent<TheSpace.SpaceObject>().power = 10;
                }
                selectedObject = _newStar;
                selectedObject.GetComponent<TheSpace.SpaceObject>().screeningObject = true;
            }
        }
    }

    private void TransitionToABackStageOfLife(SpaceObject.TypeStar type) //Переход на предыдущую стадию жизни звезды
    {
        selectedObject.GetComponent<TheSpace.SpaceObject>().backStage = false;
        if(type == SpaceObject.TypeStar.Light)
        {
            if(selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar == 2)
            {
                selectedObject = _lastStageStar;
            }
            else
            {
                --selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar;
                selectedObject = stageOfAStartLife_For_LightStar[selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar];
            }
            selectedObject.SetActive(true);  
            selectedObject.GetComponent<TheSpace.SpaceObject>().onSelected = true;    
            selectedObject.GetComponent<TheSpace.SpaceObject>().screeningObject = true;      
            if(_newStar != null) Destroy(_newStar);
            _newStar = _lastStageStar;
        }
        else if(type == SpaceObject.TypeStar.Heavy)
            Instantiate(stageOfAStartLife_For_HeavyStar[selectedObject.GetComponent<TheSpace.SpaceObject>().stageLifeThisStar], new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z), Quaternion.identity);
    }

    private void BlowUpStar()
    {
        _newStar = Instantiate(stageOfAStartLife_For_HeavyStar[5], new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z), Quaternion.identity);
        _newStar.name = stageOfAStartLife_For_HeavyStar[5].name;

        _newStar.GetComponent<TheSpace.SpaceObject>().onSelected = true;
        _newStar.GetComponent<TheSpace.SpaceObject>().nameObj = selectedObject.GetComponent<TheSpace.SpaceObject>().nameObj + " - Черная дыра";      
        _newStar.GetComponent<TheSpace.SpaceObject>().age = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().age;
        _newStar.GetComponent<TheSpace.SpaceObject>().mass = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().mass;
        _newStar.GetComponent<TheSpace.SpaceObject>().radius = System.Convert.ToSingle(PhysicsAstronomy.PhysicsFormuls.GetRadius(_newStar.transform.localScale.x, _newStar.GetComponent<TheSpace.SpaceObject>().power));
        _newStar.GetComponent<TheSpace.SpaceObject>().density = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().density;
        _newStar.GetComponent<TheSpace.SpaceObject>().surfaceTemperature = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().surfaceTemperature;
        _newStar.GetComponent<TheSpace.SpaceObject>().totalVelocity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().totalVelocity;
        _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundAxis;
        _newStar.GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().rotationalPeriodAroundSun;
        _newStar.GetComponent<TheSpace.SpaceObject>().surfaceGravity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().surfaceGravity;
        _newStar.GetComponent<TheSpace.SpaceObject>().escapeVelocity = stageOfAStartLife_For_LightStar[0].GetComponent<TheSpace.SpaceObject>().escapeVelocity;
        _newStar.GetComponent<TheSpace.SpaceObject>().screeningObject = true; 

        Destroy(selectedObject);
        selectedObject = _newStar;        
    }
}
