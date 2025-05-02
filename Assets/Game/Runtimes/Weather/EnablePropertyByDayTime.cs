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
            if (DayTimeController.staticTimeOfDay >= timeToDisableLight.x && DayTimeController.staticTimeOfDay <= timeToDisableLight.y)
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

