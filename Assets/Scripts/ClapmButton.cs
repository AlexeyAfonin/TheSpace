using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClapmButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private GUIManagerController guiMC;
    public enum Element {Mass, Radius, Density, SurfaceTemperature};
    public Element changeElement;
    public enum Operatinon {Plus, Minus};
    public Operatinon operatinon;
    private bool click = false;

    void Start()
    {
        guiMC =  GameObject.FindWithTag("Manager").GetComponent<GUIManagerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
        StartCoroutine(Change());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        click = false;
        StopCoroutine(Change());
    }

    IEnumerator Change() //Изменение значения
    {
        int holdDuration = 0;
        int multiplication = 1;
        int maxDuration = 100;

        while(click)
        {
            if(guiMC.selectSpaceObject != null)
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
                        case Element.Mass: guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass += 1*multiplication; break;
                        case Element.Radius: guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius += 1*multiplication; break;
                        case Element.Density: guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density += 1*multiplication; break;
                        case Element.SurfaceTemperature: guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature += 1*multiplication; break;
                    }
                }
                else if(operatinon == Operatinon.Minus)
                {
                    switch(changeElement)
                    {
                        case Element.Mass: 
                            if(guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass > 0)
                                guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().mass -= 1*multiplication; 
                            break;
                        case Element.Radius: 
                            if(guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius > 0)
                                guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().radius -= 1*multiplication;
                            break;
                        case Element.Density: 
                            if(guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density > 0)
                                guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().density -= 1*multiplication; 
                            break;
                        case Element.SurfaceTemperature: guiMC.selectSpaceObject.GetComponent<TheSpace.SpaceObject>().surfaceTemperature -= 1*multiplication; break;
                    }
                }

                holdDuration++;
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
