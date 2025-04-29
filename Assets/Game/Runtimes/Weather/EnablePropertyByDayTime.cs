using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class EnablePropertyByDayTime : MonoBehaviour
    {
        [SerializeField] private string propertyName;
        [SerializeField] private Vector2 timeToDisableLight;
        [SerializeField] private Material material;

        private void Update()
        {
            if (WeatherController.staticTimeOfDay >= timeToDisableLight.x && WeatherController.staticTimeOfDay <= timeToDisableLight.y)
            {
                if(material.IsKeywordEnabled(propertyName))
                    material.DisableKeyword(propertyName);
            }
            else
            {
                if (!material.IsKeywordEnabled(propertyName))
                    material.EnableKeyword(propertyName);
            }
        }
    }
}

