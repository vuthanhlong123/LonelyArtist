using LA.Painting.PaintLibary;
using System.Threading.Tasks;
using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintingManager : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] private LAPaintingDataManager dataManager;

        [Header("Member")]
        [SerializeField] private LABrushPaintHandler brushPaintHandler;
        [SerializeField] private LAPaintingColorPicker colorPickerHandler;
        [SerializeField] private LAPaintingSampleColor sampleColorBrushHandler;
        [SerializeField] private LAPaintingShapeHandler paintingShapeHandler;

        [Header("Tool Menu")]
        [SerializeField] private LAPaintingToolMenu toolMenu;

        [Header("Control Menu")]
        [SerializeField] private LAPaintingControlMenu controlMenu;

        private ToolType currentActivateTool = ToolType.Nothing;

        [Header("Properties")]
        [SerializeField] private MeshRenderer paintBoardRender;
        [SerializeField] private Material previewMaterial;
        [SerializeField] private Texture2D baseTexture;  // Texture gốc

        private RenderTexture renderTexture;
        public RenderTexture GetRenderTex => renderTexture;

        public LABrushPaintHandler BrushPaintHandler => brushPaintHandler;
        public LAPaintingColorPicker ColorPickerHandler => colorPickerHandler;
        public LAPaintingSampleColor SampleColorBrushHandler => sampleColorBrushHandler;

        public LAPaintingShapeHandler PaintingShapeHandler => paintingShapeHandler;

        [Header("Controls")]
        [SerializeField] private LAPaintControl_Undo undoControl;
        [SerializeField] private LAPaintControl_NewPaint newPaintControl;
        [SerializeField] private LAPaintControl_Saving savingControl;
        [SerializeField] private LAPaintControl_Libary paintLibaryControl;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            // Tạo RenderTexture để lưu kết quả
            renderTexture = new RenderTexture(1024, 1024, 0);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();

            // Copy Texture gốc vào RenderTexture
            Graphics.Blit(baseTexture, renderTexture);

            previewMaterial.SetTexture("_MainTex", renderTexture);
            paintBoardRender.sharedMaterial = previewMaterial;

            brushPaintHandler.StartUp(renderTexture);
            paintingShapeHandler.StartUp(renderTexture);
            dataManager.StarUp(renderTexture);
            SaveState();
        }

        public void NewPaint()
        {
            Graphics.Blit(baseTexture, renderTexture);
            dataManager.ResetPaintData();
            SaveState();
        }

        public void ContinuePaintTexture(Texture2D texture)
        {
            Graphics.Blit(texture, renderTexture);
            dataManager.ResetPaintData();
            SaveState();
        }

        public void SaveState()
        {
            dataManager.SavePaintingState();
        }

        private void OnEnable()
        {
            toolMenu.OnSubmitChangeTool += ToolMenu_OnSubmitChangeTool;
            controlMenu.OnSubmitControlRequest += ControlMenu_OnSubmitControlRequest;
            colorPickerHandler.OnChangedColor += ColorPicker_OnChangedColor;
        }

        //Painting control activation
        #region Handling painting control (like undom ...)
        private async void ControlMenu_OnSubmitControlRequest(PaintingControlType type)
        {
            switch (type)
            {
                case PaintingControlType.Undo: undoControl.Execute(); break;
                case PaintingControlType.NewPaint: await newPaintControl.Execute(); break;
                case PaintingControlType.Saving: savingControl.Execute(); break;
                case PaintingControlType.Libary: paintLibaryControl.Execute(); break;
            }
        }
        #endregion

        //Painting tool activation
        #region Handling Tools (like brush pattern, color picker,...)
        private void ToolMenu_OnSubmitChangeTool(ToolType toolType)
        {
            ActivateToolByType(toolType);
        }

        private void ActivateToolByType(ToolType type)
        {
            DeActivateTool(currentActivateTool);

            switch (type)
            {
                case ToolType.ColorPicker: colorPickerHandler.Activate(true); PreviewRender(); break;
                case ToolType.Painting: brushPaintHandler.Activate(true); RenderTexture.active = renderTexture; break;
                case ToolType.GetColorSample: sampleColorBrushHandler.Activate(true); PreviewRender(); break;
                case ToolType.ShapePaint: paintingShapeHandler.Activate(true); RenderTexture.active = renderTexture; break;
            }

            currentActivateTool = type;
        }

        private void DeActivateTool(ToolType type)
        {
            switch (type)
            {
                case ToolType.ColorPicker: colorPickerHandler.Activate(false); break;
                case ToolType.Painting: brushPaintHandler.Activate(false); break;
                case ToolType.GetColorSample: sampleColorBrushHandler.Activate(false); break;
                case ToolType.ShapePaint: paintingShapeHandler.Activate(false); break;
            }
        }
        #endregion

        private void ColorPicker_OnChangedColor(UnityEngine.Color color)
        {

        }

        public void UpdatePaintMaterial(Material mat)
        {
            paintBoardRender.sharedMaterial = mat;
        }
        public void PreviewRender()
        {
            UpdatePaintMaterial(previewMaterial);
        }

        private void OnDisable()
        {
            toolMenu.OnSubmitChangeTool -= ToolMenu_OnSubmitChangeTool;
            controlMenu.OnSubmitControlRequest -= ControlMenu_OnSubmitControlRequest;
            colorPickerHandler.OnChangedColor -= ColorPicker_OnChangedColor;
        }
    }
}


