using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtimes.Weather
{
    public enum DayTimeType
    {
        Light,
        Dark
    }

    public class WeatherController : MonoBehaviour
    {
        [Header("Presset")]
        [SerializeField] private Gradient AOColor;
        [SerializeField] private Gradient mainlightingColor;
        [SerializeField] private Gradient skyboxColor;

        [Header("Properties")]
        [SerializeField] private float starTime = 8; //Hours
        [SerializeField] private float speed; //finishing a day speed
        [SerializeField] private float cloudSpeed;
        [SerializeField] private Material skyboxMat;
        [SerializeField] private Light[] mainLights;

        [Space(10)]
        [SerializeField] private bool stop;
        public float currentTimeOfDay;

        public static float staticTimeOfDay;

        //Cloud handling
        private float cloudRotation;

        //Event
        public static event UnityAction OnDark;
        public static event UnityAction OnLight;

        //Handling event
        private DayTimeType dayTimeType;

        private void Start()
        {
            currentTimeOfDay = starTime;
            staticTimeOfDay = currentTimeOfDay;
            cloudRotation = 0;
            InitDayTimeType();
            HandlingEvent();
        }

        private void InitDayTimeType()
        {
            if(currentTimeOfDay >= 7 && currentTimeOfDay <=18)
            {
                dayTimeType = DayTimeType.Light;
            }
            else
            {
                dayTimeType = DayTimeType.Dark;
            }
        }

        private void Update()
        {
            if (stop) return;

            UpdateTimeOfDay();

            UpdateMainLight();
            UpdateSkyBox();
            UpdateAO();
            UpdateCloud();
            HandlingEvent();
            InitDayTimeType();
        }

        private void UpdateTimeOfDay()
        {
            currentTimeOfDay += Time.deltaTime * speed;

            if(currentTimeOfDay>=24)
            {
                currentTimeOfDay = 0;
            }

            staticTimeOfDay = currentTimeOfDay;
        }

        private void UpdateMainLight()
        {
            foreach (var light in mainLights)
            {
                light.color = mainlightingColor.Evaluate(currentTimeOfDay / 24);
            }
        }

        private void UpdateSkyBox()
        {
            skyboxMat.SetColor("_Tint", skyboxColor.Evaluate(currentTimeOfDay / 24));
        }
        
        private void UpdateAO()
        {
            RenderSettings.ambientLight = AOColor.Evaluate(currentTimeOfDay / 24);
        }

        private void UpdateCloud()
        {
            cloudRotation += Time.deltaTime * speed;
            if(cloudRotation >= 360) cloudRotation = 0;

            skyboxMat.SetFloat("_Rotation", cloudRotation);
        }

        private void HandlingEvent()
        {
            if (currentTimeOfDay >= 7 && currentTimeOfDay <= 18 && dayTimeType == DayTimeType.Dark)
            {
                OnLight?.Invoke();
            }
            else if((currentTimeOfDay <7 || currentTimeOfDay > 18) && dayTimeType == DayTimeType.Light)
            {
                OnDark?.Invoke();
            }
        }
    }
}
