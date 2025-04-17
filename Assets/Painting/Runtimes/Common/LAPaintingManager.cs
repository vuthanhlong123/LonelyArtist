using UnityEngine;
using UnityEngine.EventSystems;

namespace LA.Painting.Common
{
    public class LAPaintingManager : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] private LAPaintingDataManager dataManager;

        [Header("Tools")]
        [SerializeField] private LayerMask paintLayer;
        [SerializeField] private LAPaintingColorPicker colorPickerTool;
        [SerializeField] private LAPaintingBrushController brushTool;
        [SerializeField] private LAPaintingSampleColor sampleColorTool;

        [Header("Tool Menu")]
        [SerializeField] private LAPaintingToolMenu toolMenu;

        [Header("Control Menu")]
        [SerializeField] private LAPaintingControlMenu controlMenu;

        private ToolType currentActivateTool = ToolType.Nothing;

        [Header("Properties")]
        public Material paintMaterial; // Material sử dụng Shader vẽ
        public Texture2D baseTexture;  // Texture gốc
        public Texture2D brushTex;
        public RenderTexture renderTexturePreview; // RenderTexture lưu trạng thái

        private RenderTexture renderTexture;
        public RenderTexture GetRenderTex => renderTexture;

        private Vector2 currentPos;
        private Vector2 direction;
        private bool isHasChange;

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

            // Gán RenderTexture vào Material
            paintMaterial.SetTexture("_MainTex", renderTexture);
            dataManager.SavePaintingState(renderTexture);
        }

        void Update()
        {
            if (IsMouseOverUI()) return;

            if (currentActivateTool != ToolType.Painting) 
            { 
                isHasChange = false;
                return; 
            }

            if (Input.GetMouseButton(0)) // Nhấp chuột trái
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, paintLayer))
                {
                    if (currentPos == hit.textureCoord) return;
                    direction = hit.textureCoord - currentPos;
                    HandlePaintLine(direction);
                    currentPos = hit.textureCoord;

                    paintMaterial.SetVector("_BrushPosition", hit.textureCoord);

                    // Thực hiện vẽ lên RenderTexture
                    Graphics.Blit(null, renderTexturePreview, paintMaterial);
                    Graphics.Blit(renderTexturePreview, renderTexture);

                    isHasChange = true;
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, paintLayer))
                {
                    paintMaterial.SetVector("_BrushPosition", hit.textureCoord);
                }
            }

            if(Input.GetMouseButtonUp(0) && isHasChange)
            {
                dataManager.SavePaintingState(renderTexture);
                isHasChange = false;
            }
        }

        private void HandlePaintLine(Vector2 brushDirection)
        {

            Vector3 direction = new Vector3(brushDirection.x,0, brushDirection.y);
            // Create a quaternion from the direction vector
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Convert the quaternion to Euler angles
            Vector3 eulerAngles = rotation.eulerAngles;

            UpdateBrushAngleNormalize(eulerAngles.y+90);
        }

        private Texture2D toTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }

        void UpdateMainTexture(RenderTexture renderTexture, Texture2D mainTexture)
        {
            // Đặt RenderTexture làm Active
            RenderTexture.active = renderTexture;

            // Đọc dữ liệu từ RenderTexture
            mainTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            mainTexture.Apply();

            // Giải phóng Active RenderTexture
            RenderTexture.active = null;

            Debug.Log("Main Texture updated successfully.");
        }

        private void ConvertTexture2DToRenderTexture(Texture2D texture2D, RenderTexture renderTexture)
        {
            // Set the active RenderTexture
            RenderTexture.active = renderTexture;

            // Copy the Texture2D to the RenderTexture
            Graphics.Blit(texture2D, renderTexture);

            // Reset the active RenderTexture
            RenderTexture.active = null;
        }

        public void SaveTexture(int index)
        {
            // Chuyển RenderTexture thành Texture2D
            RenderTexture.active = renderTexturePreview;
            Texture2D savedTexture = new Texture2D(renderTexturePreview.width, renderTexturePreview.height, TextureFormat.ARGB32, false);
            savedTexture.ReadPixels(new Rect(0, 0, renderTexturePreview.width, renderTexturePreview.height), 0, 0);
            savedTexture.Apply();
            RenderTexture.active = null;

            // Lưu Texture thành PNG
            byte[] bytes = savedTexture.EncodeToPNG();
            string path = Application.persistentDataPath + $"/SavedTexture{index}.png";
            System.IO.File.WriteAllBytes(path, bytes);

            Debug.Log("Texture saved at: " + path);
        }

        private Texture2D SpriteToTexture2D(Sprite sprite)
        {
            Texture2D originalTexture = sprite.texture;

            // Step 2 (Optional): Create a copy of the Texture2D
            Texture2D copiedTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, originalTexture.mipmapCount > 1);
            copiedTexture.SetPixels(originalTexture.GetPixels());
            copiedTexture.Apply();

            return copiedTexture;
        }

        #region Painting Brush Updatable
        public void UpdateBrushColor(Color color)
        {
            paintMaterial.SetColor("_BrushColor", color);
        }

        public void UpdateBrushPatten(Texture2D pattern)
        {
            paintMaterial.SetTexture("_BrushTex", pattern);
        }

        public void UpdateBrushAngle(float angle)
        {
            paintMaterial.SetFloat("_Rotation", angle);
        }

        public void UpdateBrushAngleNormalize(float angle)
        {
            float normalizeValue = angle / (360 / 6.28f);
            paintMaterial.SetFloat("_Rotation", normalizeValue);
        }

        public void UpdateBrushSize(float size)
        {
            paintMaterial.SetFloat("_BrushSize", size);
        }

        public void UpdateBrushOpacity(float opacity)
        {
            paintMaterial.SetFloat("_Opacity", opacity);
        }
        #endregion

        private void OnEnable()
        {
            toolMenu.OnSubmitChangeTool += ToolMenu_OnSubmitChangeTool;
            controlMenu.OnSubmitControlRequest += ControlMenu_OnSubmitControlRequest;
            colorPickerTool.OnChangedColor += ColorPicker_OnChangedColor;

            brushTool.OnSubmitChangedBrushPattern += BrushController_OnSubmitChangedBrushPattern;
            brushTool.OnSubmitChangedBrushSize += BrushController_OnSubmitChangedBrushSize;
            brushTool.OnSubmitChangedBrushRotation += BrushController_OnSubmitChangedBrushRotation;
            brushTool.OnSubmitChangedBrushOpacity += BrushController_OnSubmitChangedBrushOpacity;
        }

        //Painting control activation
        #region Handling painting control (like undom ...)
        private void ControlMenu_OnSubmitControlRequest(PaintingControlType type)
        {
            switch (type)
            {
                case PaintingControlType.Undo: dataManager.HandlingUndo(renderTexture); break;
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
                case ToolType.ColorPicker: colorPickerTool.Activate(true); break;
                case ToolType.Painting: brushTool.Activate(true); RenderTexture.active = renderTexture; break;
                case ToolType.GetColorSample: sampleColorTool.Activate(true); break;
            }

            currentActivateTool = type;
        }

        private void DeActivateTool(ToolType type)
        {
            switch (type)
            {
                case ToolType.ColorPicker: colorPickerTool.Activate(false); break;
                case ToolType.Painting: brushTool.Activate(false); break;
                case ToolType.GetColorSample: sampleColorTool.Activate(false); break;
            }
        }
        #endregion

        private void ColorPicker_OnChangedColor(Color color)
        {
            UpdateBrushColor(color);
        }

        private void BrushController_OnSubmitChangedBrushPattern(Texture2D tex)
        {
            UpdateBrushPatten(tex);
        }

        private void BrushController_OnSubmitChangedBrushSize(float size)
        {
            UpdateBrushSize(size);
        }

        private void BrushController_OnSubmitChangedBrushRotation(float angle)
        {
            UpdateBrushAngle(angle);
        }

        private void BrushController_OnSubmitChangedBrushOpacity(float opacity)
        {
            UpdateBrushOpacity(opacity);
        }

        private void OnDisable()
        {
            toolMenu.OnSubmitChangeTool -= ToolMenu_OnSubmitChangeTool;
            controlMenu.OnSubmitControlRequest -= ControlMenu_OnSubmitControlRequest;
            colorPickerTool.OnChangedColor -= ColorPicker_OnChangedColor;

            brushTool.OnSubmitChangedBrushPattern -= BrushController_OnSubmitChangedBrushPattern;
            brushTool.OnSubmitChangedBrushSize -= BrushController_OnSubmitChangedBrushSize;
            brushTool.OnSubmitChangedBrushRotation -= BrushController_OnSubmitChangedBrushRotation;
            brushTool.OnSubmitChangedBrushOpacity -= BrushController_OnSubmitChangedBrushOpacity;
        }

        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}


