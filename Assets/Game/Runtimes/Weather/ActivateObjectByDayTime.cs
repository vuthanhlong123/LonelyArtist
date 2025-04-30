using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class ActivateObjectByDayTime : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private bool value;
        [SerializeField] private Vector2 timeToDisableLight;

        private void Update()
        {
            if (WeatherController.staticTimeOfDay >= timeToDisableLight.x && WeatherController.staticTimeOfDay <= timeToDisableLight.y)
            {
                if (target.activeSelf != value)
                    target.SetActive(value);
            }
            else
            {
                if (target.activeSelf == value)
                    target.SetActive(!value);
            }
        }
    }
}

