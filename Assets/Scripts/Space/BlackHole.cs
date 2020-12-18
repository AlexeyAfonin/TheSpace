using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("Притягивающиеся планеты")] 
    public List<GameObject> spaceObjects;

    [Header("Сила притяжения")] 
    [SerializeField] private float _forceOfGravity = 1000;

    private GUIManagerController _guiMC;

    private void FixedUpdate()
    {
        foreach(var obj in spaceObjects)
        {
            obj.transform.position = GetPositon(this.gameObject.transform.position, obj.GetComponent<TheSpace.SpaceObject>().distance, obj.GetComponent<TheSpace.SpaceObject>().currentAng, obj.GetComponent<TheSpace.SpaceObject>().offsetSin, obj.GetComponent<TheSpace.SpaceObject>().offsetCos); //Вращение вокруг объекта
            obj.transform.position += ((this.gameObject.transform.position - obj.transform.position).normalized * _forceOfGravity * Time.deltaTime) / (obj.GetComponent<TheSpace.SpaceObject>().mass*10);
            obj.transform.Rotate(Vector3.up * obj.GetComponent<TheSpace.SpaceObject>().linearRotationSpeed/10000);
            obj.GetComponent<TheSpace.SpaceObject>().currentAng += (_forceOfGravity / (obj.GetComponent<TheSpace.SpaceObject>().mass*100000) * Time.deltaTime);
        }
    }

    private Vector3 GetPositon(Vector3 around, float dist, float angle, float sin, float cos) //Получение позции
    {
        around.x += Mathf.Sin(angle) * dist * sin;
        around.z += Mathf.Cos(angle) * dist * cos;
        return around;
    }

    private void Update() 
    {
        if(this.gameObject.transform.localScale.x >= 1000000f) 
            Destroy(this.gameObject);
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 0.1f, this.gameObject.transform.localScale.y + 0.1f, this.gameObject.transform.localScale.z + 0.1f); 
        _forceOfGravity += this.gameObject.transform.localScale.x / 100f;
        this.gameObject.GetComponent<TheSpace.SpaceObject>().radius = System.Convert.ToSingle(PhysicsAstronomy.PhysicsFormuls.GetRadius(this.gameObject.transform.localScale.x, this.gameObject.GetComponent<TheSpace.SpaceObject>().power));
        _guiMC.radius.text = this.gameObject.GetComponent<TheSpace.SpaceObject>().radius.ToString() + " км";
    }

    private void OnTriggerEnter(Collider other) 
    {
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + other.GetComponent<TheSpace.SpaceObject>().density*10, this.gameObject.transform.localScale.y + other.GetComponent<TheSpace.SpaceObject>().density*10, this.gameObject.transform.localScale.z + other.GetComponent<TheSpace.SpaceObject>().density*10); 
        spaceObjects.Remove(other.gameObject);
        GameObject buttonSelected = GameObject.Find(other.name + "_UIImage");
        Destroy(buttonSelected);
        Destroy(other.gameObject);
    }
}