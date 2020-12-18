using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClapmButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private GUIManagerController _guiMC;
    public enum Element {Mass, Radius, Density, SurfaceTemperature};
    public Element changeElement;
    public enum Operatinon {Plus, Minus};
    public Operatinon operatinon;
    private bool _click = false;

    private void Start()
    {
        _guiMC =  GameObject.FindWithTag("Manager").GetComponent<GUIManagerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _click = true;
        StartCoroutine(Change());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _click = false;
        StopCoroutine(Change());
    }

    private IEnumerator Change() //Изменение значения
    {
        int holdDuration = 0;
        int multiplication = 1;
        int maxDuration = 100;

        while(_click)
        {
            if(_guiMC.selectSpaceObject != null)
            {
                if(holdDuration >= maxDuration)
                {
                    holdDuration = 0;
                    multiplication *= 10;
                    if(maxDuration < 1000) maxDuration *= 10;
                    if(multiplication >= 100) multiplication = 10;
                }

                if(operatinon == Operatinon.Plus)
                {
                    switch(changeElement)
                    {
                        case Element.Mass: _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass += 1*multiplication; break;
                        case Element.Radius: _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius += 1*multiplication; break;
                        case Element.Density: _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density += 1*multiplication; break;
                        case Element.SurfaceTemperature: _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature += 1*multiplication; break;
                    }
                }
                else if(operatinon == Operatinon.Minus)
                {
                    switch(changeElement)
                    {
                        case Element.Mass: 
                            if(_guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass > 0)
                                _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass -= 1*multiplication; 
                            break;
                        case Element.Radius: 
                            if(_guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius > 0)
                                _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius -= 1*multiplication;
                            break;
                        case Element.Density: 
                            if(_guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density > 0)
                                _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density -= 1*multiplication; 
                            break;
                        case Element.SurfaceTemperature: _guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature -= 1*multiplication; break;
                    }
                }

                holdDuration++;
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
