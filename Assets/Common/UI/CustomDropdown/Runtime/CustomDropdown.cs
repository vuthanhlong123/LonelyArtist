using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.Common.UI.Runtime
{
    public class CustomDropdown : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image_Selected;

        [SerializeField] private DropdownItemDataHolder dropdownItemDataHolder;

        private CustomDropdownScroll dropdownScroll;

        private void Start()
        {
            Init();
            CreateDropdownItems();
            ApplyDefaultState();
        }

        private void Init()
        {
            dropdownScroll = GetComponentInChildren<CustomDropdownScroll>();
            dropdownScroll.onSubmitSelected += DropdownScroll_onSubmitSelected;
        }

        private void DropdownScroll_onSubmitSelected(CustomDropdownItem selectedItem)
        {
            DropdownItemData data = dropdownItemDataHolder.GetByArrId(selectedItem.id);
            if (data == null) return;
            image_Selected.sprite = data.sprite;

            dropdownScroll.Hide();
        }

        private void CreateDropdownItems()
        {
            if (dropdownScroll == null) return;

            dropdownScroll.CreateDropdownItem(dropdownItemDataHolder.Datas);
        }

        private void ApplyDefaultState()
        {
            if (dropdownScroll != null) dropdownScroll.Hide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (dropdownScroll != null)
            {
                if(!dropdownScroll.gameObject.activeSelf)
                    dropdownScroll.Show();
                else dropdownScroll.Hide();
            }
        }
    }
}


