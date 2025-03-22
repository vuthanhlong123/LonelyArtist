using UnityEngine;
using UnityEngine.Events;

namespace LA.Painting.Common
{
    public enum PaintingControlType
    {
        Nothing,
        Undo
    }

    public class LAPaintingControlMenu : MonoBehaviour
    {
        [SerializeField] private LAPaintingControlItem[] controlItems;

        public event UnityAction<PaintingControlType> OnSubmitControlRequest;

        private void OnEnable()
        {
            foreach (var item in controlItems)
            {
                item.OnClicked += Item_OnClicked;
            }
        }

        private void Item_OnClicked(LAPaintingControlItem arg0)
        {
            OnSubmitControlRequest?.Invoke(arg0.Type);
        }

        private void OnDisable()
        {
            foreach (var item in controlItems)
            {
                item.OnClicked -= Item_OnClicked;
            }
        }
    }
}


