using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtimes.Weather
{
    public enum DayTimeType
    {
        Light,
        Dark
    }

    public class DayTimeController : MonoBehaviour
    {
        public static DayTimeController instance;

        [Header("Presset")]
        [SerializeField] private Gradient AOColor;

        [Header("Properties")]
        [SerializeField] private float starTime = 8; //Hours
        [SerializeField] private float speed; //finishing a day speed

        [Space(10)]
        [SerializeField] private bool stop;

        [Range(0,24)]
        public float currentTimeOfDay;

        public static float staticTimeOfDay;

        //Cloud handling
        private float cloudRotation;

        //Event
        public static event UnityAction OnDark;
        public static event UnityAction OnLight;

        //Handling event
        private DayTimeType dayTimeType;

        private void Awake()
        {
            instance = this;
        }

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

            UpdateAO();
            HandlingEvent();
            InitDayTimeType();
        }

        public void SetTimeOfDay(float time)
        {
            currentTimeOfDay = time;
        }

        public void StopRun(bool stop)
        {
            this.stop = stop;
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

        private void UpdateAO()
        {
            RenderSettings.ambientLight = AOColor.Evaluate(currentTimeOfDay / 24);
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
