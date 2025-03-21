using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.Common.UI.Runtime
{
    public class CustomDropdownItem : MonoBehaviour, IPointerClickHandler
    {
        public int id { get; set; }
        [SerializeField] private Image image_Icon;

        public DropdownItemData ItemData { get; set; }
        public event UnityAction<CustomDropdownItem> onSelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            onSelected?.Invoke(this);
        }

        public virtual void SetValue(DropdownItemData data)
        {
            image_Icon.sprite = data.sprite;
            ItemData = data;
        }
    }
}


