using UnityEngine;

namespace Game.Runtimes.Weather
{
    public class UpdateRotationByDayTime : MonoBehaviour
    {
        [SerializeField] private Transform targetTrans;
        [SerializeField] private Vector3 start;
        [SerializeField] private Vector3 end;

        private void Update()
        {
            Vector3 euler = Vector3.Lerp(start, end, DayTimeController.staticTimeOfDay/24);
            targetTrans.localEulerAngles = euler;
        }
    }
}

