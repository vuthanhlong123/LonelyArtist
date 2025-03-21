using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.Common.UI.Runtime
{
    public class CustomDropdown : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image_Selected;

        [SerializeField] private DropdownItemDataHolder dropdownItemDataHolder;

        [SerializeField] private CustomDropdownScroll dropdownScroll;

        //Events
        public event UnityAction<CustomDropdownItem> OnSelectionChange;
        public event UnityAction OnDropdownClick;

        private void Start()
        {
            CreateDropdownItems();
            ApplyDefaultState();
        }

        private void CreateDropdownItems()
        {
            if (dropdownItemDataHolder == null) return;

            if (dropdownScroll == null) return;

            dropdownScroll.CreateDropdownItem(dropdownItemDataHolder.Datas);
        }

        public void CreateDropdownItems(DropdownItemData[] itemDatas)
        {
            if (dropdownScroll == null) return;

            dropdownScroll.CreateDropdownItem(itemDatas);
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
                OnDropdownClick?.Invoke();
            }
        }

        public void Show()
        {
            dropdownScroll.Show();
        }

        public void Hide()
        {
            dropdownScroll.Hide();
        }

        private void OnEnable()
        {
            dropdownScroll.onSubmitSelected += DropdownScroll_onSubmitSelected;
        }

        private void DropdownScroll_onSubmitSelected(CustomDropdownItem selectedItem)
        {
            image_Selected.sprite = selectedItem.ItemData.sprite;

            OnSelectionChange?.Invoke(selectedItem);

            dropdownScroll.Hide();
        }

        private void OnDisable()
        {
            dropdownScroll.onSubmitSelected -= DropdownScroll_onSubmitSelected;
        }
    }
}


