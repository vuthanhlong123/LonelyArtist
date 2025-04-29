using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdatingLightByDayTime : MonoBehaviour
    {
        [SerializeField] private Light targetLight;
        [SerializeField] private Vector2 timeToDisableLight;

        private void Update()
        {
            if (WeatherController.staticTimeOfDay >= timeToDisableLight.x && WeatherController.staticTimeOfDay <= timeToDisableLight.y )
            {
                if(targetLight.enabled)
                {
                    targetLight.enabled = false;
                }
            }
            else 
            {
                if (!targetLight.enabled)
                {
                    targetLight.enabled = enabled;
                }
            }
        }
    }
}

