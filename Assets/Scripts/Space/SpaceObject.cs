namespace TheSpace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using PhysicsAstronomy;

    public class SpaceObject : MonoBehaviour
    {
        private GUIManagerController _guiMC;
        private GameManagerController _gameMC;
        private StarEvolution _starEvolution;
        public enum TypeObject {Planet, Satellite, Star, BlackHole}; //Список типов космических объектов
        public enum TypeStar {Light, Heavy}; //Список типов звезд

        [Header("Вращение")]
        public Transform aroundObject;
        public float offsetSin = 1, offsetCos = 1;
        [HideInInspector] public float distance, currentAng;

        [Header("Параметры")]
        public TypeObject typeThisObject; //Тип космического объекта
        public string nameObj; //Название
        public float age; //Возраст
        public float mass; //Масса плнеты
        public float radius; //Радиус
        public float density; //Плотность
        public float surfaceTemperature; //Средняя температура
        public float totalVelocity; //Общая скорость
        public float rotationalPeriodAroundAxis; //Период вращения вокруг своей оси
        public float rotationalPeriodAroundSun; //Период вращения вокруг Солнца
        public float surfaceGravity; //Сила притяжения (сила ускорения свободного падения) | GM/R²
        public float firstSpaceSpeed; //Первая космическая скорость
        public float escapeVelocity; //Вторая космическая скорость (скорость убегания)
        [SerializeField] private float _distanceToTheAroundObject; //Расстояние до солнца (N млн км)
        public float linearRotationSpeed; //Сокрость линейного вращения (вокруг своей оси)

        [Header ("Состояния")]
        public bool onSelected;
        public bool exploded;

        [Header ("Этап жизни (ЕСЛИ ОБЪЕКТ - ЗВЕЗДА)")]
        public TypeStar typeThisStar; //Тип звезды
        public int stageLifeThisStar;
        public bool nextStage;
        public bool backStage;

        [Header ("Настройки")]
        public int power;
        
        private GameObject _blackHole;
        private bool _attracts;
        [HideInInspector] public bool screeningObject;
        private Rigidbody _rigidBody;
        

        private void Start()
        {
            _guiMC = GameObject.FindWithTag("Manager").GetComponent<GUIManagerController>();
            _gameMC = GameObject.FindWithTag("Manager").GetComponent<GameManagerController>();
            _starEvolution = GameObject.FindWithTag("Manager").GetComponent<StarEvolution>();
            _rigidBody = this.gameObject.GetComponent<Rigidbody>();

            if(aroundObject != null) distance = _distanceToTheAroundObject * 10; //дистанция между этим объектом и объектом, вогруг которого вращаемся
        }

        private void Update()
        {
            /*Вычисления*/
            totalVelocity = Convert.ToSingle(PhysicsFormuls.GetTotalVelocity(_distanceToTheAroundObject, rotationalPeriodAroundSun));
            surfaceGravity = Convert.ToSingle(PhysicsFormuls.GetSurfaceGravity(mass, radius));
            escapeVelocity = Convert.ToSingle(PhysicsFormuls.GetSecondSpaceSpeed(surfaceGravity, radius));
            firstSpaceSpeed = Convert.ToSingle(PhysicsFormuls.GetFirstSpaceSpeed(mass, radius));
            linearRotationSpeed = Convert.ToSingle(PhysicsFormuls.GetLinerRotationSpeed(rotationalPeriodAroundAxis, radius));
            _rigidBody.mass = mass/100;

            if(typeThisObject != TypeObject.BlackHole)
            {
                float sphereVolume = Convert.ToSingle(PhysicsFormuls.GetSphereVolume(radius, power)); //Определяем размер планеты
                this.transform.localScale = new Vector3(sphereVolume, sphereVolume, sphereVolume); //Записываем его, преобразуя в размер игрового объекта
            }

            /*Отправка данных в боковую UI панель*/
            if(onSelected)
            {
                _guiMC.selectSpaceObject = this.gameObject;
                _gameMC.selectedObject = this.gameObject;
                _starEvolution.selectedObject = this.gameObject;

                if(typeThisObject == TypeObject.Planet)  _guiMC.type.text = "Планета";
                if(typeThisObject == TypeObject.Satellite)  _guiMC.type.text = "Спутник";
                if(typeThisObject == TypeObject.Star)  _guiMC.type.text = "Звезда";
                if(typeThisObject == TypeObject.BlackHole)  _guiMC.type.text = "Черная дыра";
               
                _guiMC.nameObj.text = nameObj;
                _guiMC.age.text = age.ToString() + " млн лет";
                _guiMC.mass.text = mass.ToString() + " * 10²⁴ кг";
                _guiMC.radius.text = radius.ToString() + " км";
                _guiMC.density.text = density.ToString() + " г/см³";
                _guiMC.surfaceTemperature.text = surfaceTemperature.ToString() + " °С";
                _guiMC.totalVelocity.text = totalVelocity.ToString() + " км/сек";
                _guiMC.rotationalPeriodAroundAxis.text = rotationalPeriodAroundAxis.ToString() + " часов";
                _guiMC.rotationalPeriodAroundSun.text = rotationalPeriodAroundSun.ToString() + " дней";
                _guiMC.surfaceGravity.text = surfaceGravity.ToString() + " м/сек²";
                _guiMC.escapeVelocity.text = escapeVelocity.ToString() + " км/сек";

                onSelected = false;
                if(screeningObject) 
                {
                    StartCoroutine(CreatePhotoStar());
                }
            }
        }

        private void FixedUpdate()
        {
            /*Взаимодействие с черной дырой*/
            if((_blackHole != null)&&(typeThisObject != TypeObject.BlackHole))
            {
                if(!_attracts) 
                {
                    _blackHole.GetComponent<BlackHole>().spaceObjects.Add(this.gameObject);
                    aroundObject = _blackHole.transform;
                    _attracts = true;
                }   
            }
            else 
            {
                _blackHole = GameObject.FindGameObjectWithTag("BlackHole"); //Ищем черную дыру на сцене

                if((aroundObject != null)&&(_blackHole == null)) //Вращение объекта по орбите
                {
                    this.transform.position = GetPositon(aroundObject.position, distance, currentAng, offsetSin, offsetCos); //Вращение вокруг объекта
                    this.transform.Rotate(Vector3.up * linearRotationSpeed/1000); //Вращение вокруг своей оси
                    currentAng += (totalVelocity/1000) * Time.deltaTime;
                }
            }
        }

        private Vector3 GetPositon(Vector3 around, float dist, float angle, float sin, float cos) //Получение позции
        {
            around.x += Mathf.Sin(angle) * dist * sin;
            around.z += Mathf.Cos(angle) * dist * cos;
            return around;
        }

        public void ClickOnImage_WithThisSpaceObject()
        {
            onSelected = true;
        } 

        private IEnumerator  CreatePhotoStar()
        {
            yield return new WaitForSecondsRealtime(1f);
            
            _guiMC.excretion.SetActive(false);

            // Создаем камеру, с которой будем снимать
            Camera camera = new GameObject("ScreenShotCamera", typeof(Camera)).GetComponent<Camera>();

            // Позиционируем ее над террейном
            camera.transform.position = new Vector3(this.gameObject.transform.position.x - this.gameObject.transform.localScale.x/2, this.gameObject.transform.localScale.y/2, this.gameObject.transform.position.z - this.gameObject.transform.localScale.z/2);
            camera.transform.LookAt(this.gameObject.transform);
            camera.backgroundColor = Color.black;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.orthographic = false;
            camera.farClipPlane = 10000f;

            // Создаем текстуру, в которую будем рендерить
            // (Предварительно сохранив текущую)
            RenderTexture currentRT = RenderTexture.active;
            // Создаем текстуру, пропорциональную размеру террейна
            RenderTexture rt = new RenderTexture(2048, 2048, 10000);

            // Устанавливаем созданную текстуру как целевую
            RenderTexture.active = rt;
            camera.targetTexture = rt;

            // Принудительно вызываем рендер камеры
            camera.GetComponent<Camera>().Render();

            // Получаем обычную текстуру из RenderTexture'ы
            // Ее можно будет использовать в игре, или же
            // Сохранить, что мы и сделаем
            Texture2D image = new Texture2D(rt.width, rt.height);
            image.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            image.Apply();

            yield return new WaitForSecondsRealtime(0.5f); //Ждем прогрузки шейдеров

            // Пишем текстуру в .png файл
            byte[] bytes = image.EncodeToPNG();
#if !UNITY_EDITOR
            System.IO.Directory.CreateDirectory(Application.dataPath + "/Resources/Image");
#endif
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Image/" + this.gameObject.name + ".png", bytes);
            // Восстанавливаем рендер таргет
            RenderTexture.active = currentRT;

            // Чистим все (при необходимости)
            DestroyImmediate(image);
            DestroyImmediate(camera.gameObject);
            DestroyImmediate(rt);

            screeningObject = false;
            _guiMC.excretion.SetActive(true);
            
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(UpdateButton());
            StopCoroutine(CreatePhotoStar());
        }
        private IEnumerator UpdateButton()
        {
            //Меняем изображене кнопки выбора объекта
            GameObject buttonSelect = GameObject.Find("Sun_UIImage");
            buttonSelect.GetComponent<UnityEngine.UI.RawImage>().texture = Resources.Load("Image/" + this.gameObject.name) as Texture2D;

            //Меняем выполняемый метод
            UnityEngine.UI.Button btn = buttonSelect.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate{this.ClickOnImage_WithThisSpaceObject();});

            yield return new WaitForSecondsRealtime(1f);
            StopCoroutine(UpdateButton());
        }

        /*private void OnCollisionEnter(Collision other) 
        {
            other.gameObject.GetComponent<TheSpace.SpaceObject>().exploded = true;
            exploded = true;
        }*/
    }
}