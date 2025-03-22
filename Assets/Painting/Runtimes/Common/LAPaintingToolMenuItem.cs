using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public class LAPaintingToolMenuItem : MonoBehaviour, IPointerClickHandler
    {
        public ToolType toolType;
        [SerializeField] private Image image_HighLight;

        public bool isActivate;

        public event UnityAction<LAPaintingToolMenuItem> OnClickEvent;

        private void Awake()
        {
            ForceActivate(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(this);
        }

        public void ForceActivate(bool state)
        {
            isActivate = state;
            image_HighLight.enabled = state;
        }

        public void ActivateHighLight(bool state)
        {
            isActivate = !state;
            image_HighLight.enabled = state;
        }
    }
}


