using LA.Common.Tools;
using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintingSampleColor : LAPaintingTool
    {
        [Space(15)]
        [SerializeField] private RectTransform brush_Trans;
        [SerializeField] private LayerMask targetLayer;

        private void Update()
        {
            UpdateBrushPosition();
            HandlingGetColorSample();
        }

        private void UpdateBrushPosition()
        {
            brush_Trans.anchoredPosition = Input.mousePosition;
        }

        private void HandlingGetColorSample()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayer))
                {
                    Renderer renderer = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (renderer != null && meshCollider != null)
                    {
                        Texture2D texture2d = CustomRenderUtility.RenderTexturetoTexture2D(paintManager.GetRenderTex);

                        Vector2 uv = hit.textureCoord;
                        Color color = texture2d.GetPixelBilinear(uv.x, uv.y);
                        paintManager.ColorPickerHandler.UpdateColor(color);
                    }
                }
            }
        }
    }
}


