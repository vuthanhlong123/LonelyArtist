using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class SetFloatProperyZeroAndOneByDayTime : MonoBehaviour
    {
        [SerializeField] private string propertyName;
        [SerializeField] private Vector2 timeToDisableLight;
        [SerializeField] private Material material;

        private void Update()
        {
            if (DayTimeController.staticTimeOfDay >= timeToDisableLight.x && DayTimeController.staticTimeOfDay <= timeToDisableLight.y)
            {
                if(material.HasFloat(propertyName))
                    material.SetFloat(propertyName, 0);
            }
            else
            {
                if (material.HasFloat(propertyName))
                    material.SetFloat(propertyName, 1);
            }
        }
    }

}
