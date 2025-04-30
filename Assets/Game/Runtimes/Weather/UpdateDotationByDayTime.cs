using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdateDotationByDayTime : MonoBehaviour
    {
        [SerializeField] private Transform targetTrans;
        [SerializeField] private Vector3 start;
        [SerializeField] private Vector3 end;

        private void Update()
        {
            Vector3 euler = Vector3.Lerp(start, end, WeatherController.staticTimeOfDay/24);
            targetTrans.localEulerAngles = euler;
        }
    }
}

