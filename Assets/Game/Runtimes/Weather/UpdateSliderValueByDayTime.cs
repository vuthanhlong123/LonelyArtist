using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Runtimes.Weather
{
    public class UpdateSliderValueByDayTime : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Slider slider;

        private bool isDargging = false;

        private void Start()
        {
            if(slider == null) slider = GetComponent<Slider>();
        }

        void Update()
        {
            if (isDargging) return;

            slider.value=DayTimeController.staticTimeOfDay;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDargging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            DayTimeController.instance.SetTimeOfDay(slider.value);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDargging = false;
        }
    }
}

