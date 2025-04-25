using UnityEngine;

namespace LA.Painting.Common
{
    public enum ToolType
    {
        Nothing,
        Painting,
        ColorPicker,
        GetColorSample,
        ShapePaint
    }

    public class LAPaintingTool : MonoBehaviour
    {
        [SerializeField] protected LAPaintingManager paintManager;
        [SerializeField] protected ToolType toolType;
        public ToolType ToolType => ToolType;
        protected virtual void Awake()
        {
            Activate(false);
        }
        public virtual void StartUp(RenderTexture renderTex)
        {

        }

        public virtual void Activate(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}


