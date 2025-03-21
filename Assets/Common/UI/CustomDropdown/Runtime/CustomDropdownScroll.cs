using UnityEngine;
using UnityEngine.Events;

namespace LA.Common.UI.Runtime
{
    public class CustomDropdownScroll : MonoBehaviour
    {
        [SerializeField] private Transform contentHodler;
        [SerializeField] private GameObject itemSample;

        public event UnityAction<CustomDropdownItem> onSubmitSelected;

        public void CreateDropdownItem(DropdownItemData[] itemDatas)
        {
            for (int i = 0; i < itemDatas.Length; i++)
            {
                GameObject newItem = Instantiate(itemSample, contentHodler);
                newItem.name = $"Item {i}";

                CustomDropdownItem dropdownItemComponent = newItem.GetComponent<CustomDropdownItem>();
                if (dropdownItemComponent != null)
                {
                    dropdownItemComponent.id = i;
                    dropdownItemComponent.SetValue(itemDatas[i]);
                    dropdownItemComponent.onSelected += DropdownItem_onSelected;
                }
            }
        }

        private void DropdownItem_onSelected(CustomDropdownItem selectedItem)
        {
            onSubmitSelected?.Invoke(selectedItem);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}


