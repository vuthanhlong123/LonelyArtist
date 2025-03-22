using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LA.Painting.Common
{
    public class LAPaintingControlItem : MonoBehaviour, IPointerClickHandler
    {
        public PaintingControlType Type;

        public event UnityAction<LAPaintingControlItem> OnClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}


