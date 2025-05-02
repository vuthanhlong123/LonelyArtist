using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtimes.Weather
{
    public class StopRunningDayTime : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        private void Start()
        {
            if(toggle == null)
            {
                toggle = GetComponent<Toggle>();
            }

            if(toggle)
            {
                toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); });
            }

            OnToggleValueChanged(toggle);
        }

        private void OnToggleValueChanged(Toggle toggle)
        {
            DayTimeController.instance.StopRun(toggle.isOn);
        }
    }
}


