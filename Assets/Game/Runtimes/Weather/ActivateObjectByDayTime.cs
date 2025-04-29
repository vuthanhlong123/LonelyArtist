using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class ActivateObjectByDayTime : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Vector2 timeToDisableLight;

        private void Update()
        {
            if (WeatherController.staticTimeOfDay >= timeToDisableLight.x && WeatherController.staticTimeOfDay <= timeToDisableLight.y)
            {
                if (target.activeSelf)
                    target.SetActive(false);
            }
            else
            {
                if (!target.activeSelf)
                    target.SetActive(true);
            }
        }
    }
}

