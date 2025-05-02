using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdatingLightByDayTime : MonoBehaviour
    {
        [SerializeField] private Light targetLight;
        [SerializeField] private Vector2 timeToDisableLight;

        private void Update()
        {
            if (DayTimeController.staticTimeOfDay >= timeToDisableLight.x && DayTimeController.staticTimeOfDay <= timeToDisableLight.y )
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

