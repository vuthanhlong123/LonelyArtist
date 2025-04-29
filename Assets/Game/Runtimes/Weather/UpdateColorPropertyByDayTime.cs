using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdateColorPropertyByDayTime : MonoBehaviour
    {
        [SerializeField] private string propertyName = "_EmissionColor";
        [SerializeField] private Material material;
        [SerializeField] private Gradient color;
        [SerializeField] private float intensity;

        private void Update()
        {
            if(material.HasColor(propertyName))
            {
                material.SetColor(propertyName, color.Evaluate(WeatherController.staticTimeOfDay / 24)* intensity);
            }
        } 
    }
}

