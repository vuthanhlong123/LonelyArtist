using UnityEngine;
using UnityEngine.EventSystems;

namespace LA.Painting.Common
{
    public class LAPaintingShapeHandler : LAPaintingTool
    {
        [Header("Painting setup")]
        [SerializeField] private LayerMask paintLayer;
        [SerializeField] private Material paintMaterial; // Material sử dụng Shader vẽ
        [SerializeField] private RenderTexture renderTexturePreview; // RenderTexture lưu trạng thái

        [Header("UI")]
        [SerializeField] private LAShapePaintUI brushPaintUI;

        private RenderTexture renderTexture;
        public RenderTexture GetRenderTex => renderTexture;

        private Vector2 startPos;
        private Vector2 currentPos;

        public override void StartUp(RenderTexture renderTex)
        {
            // Gán RenderTexture vào Material
            renderTexture = renderTex;
            paintMaterial.SetTexture("_MainTex", renderTex);
        }

        public override void Activate(bool state)
        {
            paintManager.UpdatePaintMaterial(paintMaterial);

            ResetSize();

            UpdateShapeColor(paintManager.ColorPickerHandler.currentColor);
            gameObject.SetActive(state);
        }

        void Update()
        {
            if (IsMouseOverUI()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, paintLayer))
                {
                    startPos = hit.textureCoord;
                    currentPos = hit.textureCoord;
                }
            }

            if (Input.GetMouseButton(0)) // Nhấp chuột trái
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, paintLayer))
                {
                    if (currentPos == hit.textureCoord) return;

                    currentPos = hit.textureCoord;

                    Vector2 shapePos = Vector2.Lerp(startPos, currentPos, 0.5f);
                    paintMaterial.SetVector("_BrushPosition", shapePos);

                    Vector2 shapeScale = new Vector2(Mathf.Abs(currentPos.x - startPos.x), Mathf.Abs(currentPos.y - startPos.y));
                    paintMaterial.SetVector("_BrushSize", shapeScale);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Thực hiện vẽ lên RenderTexture
                Graphics.Blit(null, renderTexturePreview, paintMaterial);
                Graphics.Blit(renderTexturePreview, renderTexture);

                paintManager.SaveState();
                ResetSize();
            }
        }

        private void ResetSize()
        {
            Vector2 shapeScale = new Vector2(0, 0);
            paintMaterial.SetVector("_BrushSize", shapeScale);
        }

        #region Painting Brush Updatable
        public void UpdateShapeColor(Color color)
        {
            paintMaterial.SetColor("_BrushColor", color);
        }

        public void UpdateShapePatten(Texture2D pattern)
        {
            paintMaterial.SetTexture("_BrushTex", pattern);
        }

        public void UpdateShapeOpacity(float opacity)
        {
            paintMaterial.SetFloat("_Opacity", opacity);
        }

        //Width of shape's edge
        public void UpdateShapeWireWidth(float width)
        {
            if(paintMaterial.HasFloat("_WireWidth"))
                paintMaterial.SetFloat("_WireWidth", width);
        }

        public void UpdateShapeSide(float side)
        {
            if (paintMaterial.HasFloat("_Side"))
                paintMaterial.SetFloat("_Side", side);
        }

        // Radius of shape's corner
        public void UpdateShapeRadius(float radius)
        {
            if (paintMaterial.HasFloat("_Radius"))
                paintMaterial.SetFloat("_Radius", radius);
        }
        #endregion

        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void OnEnable()
        {
            brushPaintUI.OnSubmitChangedShapePattern += BrushPaintUI_OnSubmitChangedShapePattern;
            brushPaintUI.OnSubmitChangedShapeOpacity += BrushPaintUI_OnSubmitChangedShapeOpacity;
            brushPaintUI.OnSubmitChangedWireWidth += BrushPaintUI_OnSubmitChangedWireWidth;
            brushPaintUI.OnSubmitChangedSide += BrushPaintUI_OnSubmitChangedSide;
            brushPaintUI.OnSubmitChangedBoundRadius += BrushPaintUI_OnSubmitChangedBoundRadius;
        }

        private void BrushPaintUI_OnSubmitChangedBoundRadius(float radius)
        {
            UpdateShapeRadius(radius);
        }

        private void BrushPaintUI_OnSubmitChangedSide(float side)
        {
            UpdateShapeSide(side);
        }

        private void BrushPaintUI_OnSubmitChangedWireWidth(float width)
        {
            UpdateShapeWireWidth(width);
        }

        private void BrushPaintUI_OnSubmitChangedShapeOpacity(float opacity)
        {
            UpdateShapeOpacity(opacity);
        }

        private void BrushPaintUI_OnSubmitChangedShapePattern(Texture2D pattern, Material paintMat)
        {
            paintMaterial = paintMat;
            paintManager.UpdatePaintMaterial(paintMat);
            paintMaterial.SetTexture("_MainTex", renderTexture);
            UpdateShapeColor(paintManager.ColorPickerHandler.currentColor);
            UpdateShapePatten(pattern);
            ResetSize();
        }

        private void OnDisable()
        {
            brushPaintUI.OnSubmitChangedShapePattern -= BrushPaintUI_OnSubmitChangedShapePattern;
            brushPaintUI.OnSubmitChangedShapeOpacity -= BrushPaintUI_OnSubmitChangedShapeOpacity;
            brushPaintUI.OnSubmitChangedWireWidth -= BrushPaintUI_OnSubmitChangedWireWidth;
            brushPaintUI.OnSubmitChangedSide -= BrushPaintUI_OnSubmitChangedSide;
            brushPaintUI.OnSubmitChangedBoundRadius -= BrushPaintUI_OnSubmitChangedBoundRadius;
        }
    }
}

