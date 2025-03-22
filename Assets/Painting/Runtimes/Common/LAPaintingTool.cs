using UnityEngine;

namespace LA.Painting.Common
{
    public enum ToolType
    {
        Nothing,
        Painting,
        ColorPicker,
        GetColorSample
    }

    public class LAPaintingTool : MonoBehaviour
    {
        [SerializeField] private ToolType toolType;
        public ToolType ToolType => ToolType;

        [SerializeField] private GameObject toolContainer;
        [SerializeField] private bool disableOnAwake;

        public bool isActivate;

        private void Awake()
        {
            Activate(false);
        }

        public void Activate(bool state)
        {
            isActivate = state;
            toolContainer.SetActive(isActivate);
        }
    }
}


