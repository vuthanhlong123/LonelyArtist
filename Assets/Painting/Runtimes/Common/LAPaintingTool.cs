using UnityEngine;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public enum ToolType
    {
        Painting,
        GetColorSample
    }

    public class LAPaintingTool : MonoBehaviour
    {
        [SerializeField] private ToolType toolType;
        public ToolType ToolType => ToolType;

        [SerializeField] private Button button_Activate;
        [SerializeField] private Image image_HighLight;

        public bool isActivate;

        protected virtual void Start()
        {
            if(button_Activate)
                button_Activate.onClick.AddListener(OnButtonActivateClicked);
        }

        protected virtual void OnButtonActivateClicked()
        {
            isActivate = !isActivate;

            if(image_HighLight)
                image_HighLight.enabled = isActivate;
        }
    }
}


