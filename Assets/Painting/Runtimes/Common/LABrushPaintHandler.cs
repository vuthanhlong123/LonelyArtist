using UnityEngine;
using UnityEngine.EventSystems;

namespace LA.Painting.Common
{
    public class LABrushPaintHandler : LAPaintingTool
    {
        [Header("Painting setup")]
        [SerializeField] private LayerMask paintLayer;
        [SerializeField] private Material paintMaterial; // Material sử dụng Shader vẽ
        [SerializeField] private RenderTexture renderTexturePreview; // RenderTexture lưu trạng thái

        [Header("UI")]
        [SerializeField] private LABrushPaintUI brushPaintUI;

        private RenderTexture renderTexture;
        public RenderTexture GetRenderTex => renderTexture;

        private Vector2 currentPos;
        private Vector2 direction;
        private bool isHasChange;

        public override void StartUp(RenderTexture renderTex)
        {
            // Gán RenderTexture vào Material
            renderTexture = renderTex;
            paintMaterial.SetTexture("_MainTex", renderTex);
            UpdateBrushAngle(0);
        }

        public override void Activate(bool state)
        {
            isHasChange = false;

            if(state)
            {
                paintManager.UpdatePaintMaterial(paintMaterial);
                UpdateBrushColor(paintManager.ColorPickerHandler.currentColor);
            }

            gameObject.SetActive(state);
        }
       
        void Update()
        {
            if (IsMouseOverUI()) return;

            if (Input.GetMouseButton(0)) // Nhấp chuột trái
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, paintLayer))
                {
                    if (currentPos == hit.textureCoord) return;
                    //HandlePaintLine(currentPos, hit.textureCoord);
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

            if (Input.GetMouseButtonUp(0) && isHasChange)
            {
                paintManager.SaveState();
                isHasChange = false;
            }
        }

        private void HandlePaintLine(Vector2 starPos, Vector2 targetPos)
        {

            Vector2 v1 = new Vector2(0, 1);
            Vector2 v2 = targetPos - starPos;
            v2.y = -v2.y;

            float angle = Vector2.Angle(v1, v2);
            if (v2.x > 0)
            {
                angle = -angle;
            }

            paintMaterial.SetFloat("_Rotation", -angle);

            /*Vector3 direction = new Vector3(brushDirection.x, 0, brushDirection.y);
            // Create a quaternion from the direction vector
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Convert the quaternion to Euler angles
            Vector3 eulerAngles = rotation.eulerAngles;

            UpdateBrushAngleNormalize(eulerAngles.y + 90);*/
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

        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void OnEnable()
        {
            brushPaintUI.OnSubmitChangedBrushPattern += BrushPaintUI_OnSubmitChangedBrushPattern;
            brushPaintUI.OnSubmitChangedBrushOpacity += BrushPaintUI_OnSubmitChangedBrushOpacity;
            brushPaintUI.OnSubmitChangedBrushSize += BrushPaintUI_OnSubmitChangedBrushSize;
        }

        private void BrushPaintUI_OnSubmitChangedBrushSize(float size)
        {
            UpdateBrushSize(size);
        }

        private void BrushPaintUI_OnSubmitChangedBrushOpacity(float opacity)
        {
            UpdateBrushOpacity(opacity);
        }

        private void BrushPaintUI_OnSubmitChangedBrushPattern(Texture2D pattern)
        {
            UpdateBrushPatten(pattern);
        }

        private void OnDisable()
        {
            brushPaintUI.OnSubmitChangedBrushPattern -= BrushPaintUI_OnSubmitChangedBrushPattern;
            brushPaintUI.OnSubmitChangedBrushOpacity -= BrushPaintUI_OnSubmitChangedBrushOpacity;
            brushPaintUI.OnSubmitChangedBrushSize -= BrushPaintUI_OnSubmitChangedBrushSize;
        }
    }
}

