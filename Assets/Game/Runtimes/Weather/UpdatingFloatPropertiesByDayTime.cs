using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdatingFloatPropertiesByDayTime : MonoBehaviour
    {
        [SerializeField] private string propertiesName;
        [SerializeField] private Material material;

        [SerializeField] private AnimationCurve curveValue;
        private void Update()
        {
            float value = curveValue.Evaluate(DayTimeController.staticTimeOfDay / 24);
            if(material.HasFloat(propertiesName))
            {
                material.SetFloat(propertiesName, value);
            }
        }
    }
}

