using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace LA.Painting.Common
{
    public class LAPaintingToolMenu : MonoBehaviour
    {
        [SerializeField] private LAPaintingToolMenuItem[] menuItems;

        public event UnityAction<ToolType> OnSubmitChangeTool;

        private void AddListener()
        {
            foreach (var item in menuItems)
            {
                item.OnClickEvent += Item_OnClickEvent;
            }
        }

        private void Item_OnClickEvent(LAPaintingToolMenuItem arg0)
        {
            DisableAllTool();
            arg0.ForceActivate(true);
            OnSubmitChangeTool?.Invoke(arg0.toolType);
        }

        private void DisableAllTool()
        {
            foreach (var item in menuItems)
            {
                item.ForceActivate(false);
            }
        }

        private void OnEnable()
        {
            foreach (var item in menuItems)
            {
                item.OnClickEvent += Item_OnClickEvent;
            }
        }

        private void OnDisable()
        {
            foreach (var item in menuItems)
            {
                item.OnClickEvent -= Item_OnClickEvent;
            }
        }
    }
}


