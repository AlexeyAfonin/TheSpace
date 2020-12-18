using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSpace;

public class GameManagerStagesOfALiveStars : MonoBehaviour
{
    public List<GameObject> _stageLightStars;
    public List<GameObject> _stageHeavyStars;
    private int _lastStage;
    private GameObject _activeStar;

    private void Start()
    {
        _activeStar = GameObject.FindGameObjectWithTag("Star");
    }
    
    private void Update()
    {
        GameObject oldStar;
        if(_activeStar.GetComponent<SpaceObject>().stageLifeThisStar > _lastStage)
        {
            oldStar = _activeStar;
            _lastStage++;
            if(_activeStar.GetComponent<SpaceObject>().typeThisStar == SpaceObject.TypeStar.Light)
                _activeStar = Instantiate(_stageLightStars[_lastStage], new Vector3(0, 0, 0), Quaternion.identity);
            else
                _activeStar = Instantiate(_stageHeavyStars[_lastStage], new Vector3(0, 0, 0), Quaternion.identity);

            _activeStar.transform.localScale = oldStar.transform.localScale;
            _activeStar.GetComponent<SpaceObject>().radius = oldStar.GetComponent<SpaceObject>().radius;
            _activeStar.GetComponent<SpaceObject>().power = oldStar.GetComponent<SpaceObject>().power;
            _activeStar.GetComponent<SpaceObject>().onSelected = false;
            _activeStar.GetComponent<SpaceObject>().stageLifeThisStar = _lastStage;

            Camera.main.transform.position = new Vector3(0, 0, _activeStar.transform.position.z - _activeStar.transform.localScale.z - 500);

            Destroy(oldStar);
        }
    }

    public void StageHeaveStar()
    {
       
    }
    public void StageLightStar()
    {

    }
}
