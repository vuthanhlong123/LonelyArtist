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

        /* private void OnEnable()
         {
             WeatherController.OnDark += WeatherController_OnDark;
             WeatherController.OnLight += WeatherController_OnLight;
         }

         private async void WeatherController_OnLight()
         {
             await Task.Yield();
             targetLight.enabled = false;
         }

         private async void WeatherController_OnDark()
         {
             await Task.Yield();
             targetLight.enabled = true;
         }

         private void OnDisable()
         {
             WeatherController.OnDark -= WeatherController_OnDark;
             WeatherController.OnLight -= WeatherController_OnLight;
         }*/
    }
}

