using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdateLightColorByDayTime : MonoBehaviour
    {
        [SerializeField] private Light targetLight;
        [SerializeField] private Gradient gradient;

        private void Update()
        {
            Color color = gradient.Evaluate(DayTimeController.staticTimeOfDay / 24);
            targetLight.color = color;
        }
    }
}

