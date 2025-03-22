using LA.Common.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace LA.Painting.Common
{
    public class LAPaintingSampleColor : LAPaintingTool
    {
        [Space(15)]
        [SerializeField] private LAPaintingManager paintingManager;
        [SerializeField] private RectTransform brush_Trans;
        [SerializeField] private LayerMask targetLayer;

        public event UnityAction<Color> OnGettedColorSample;

        private void Update()
        {
            UpdateBrushPosition();
            HandlingGetColorSample();
        }

        private void UpdateBrushPosition()
        {
            if (!isActivate) return;

            brush_Trans.anchoredPosition = Input.mousePosition;
        }

        private void HandlingGetColorSample()
        {
            if (!isActivate) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayer))
                {
                    Renderer renderer = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (renderer != null && meshCollider != null)
                    {
                        Texture2D texture2d = CustomRenderUtility.RenderTexturetoTexture2D(paintingManager.GetRenderTex);

                        Vector2 uv = hit.textureCoord;
                        Color color = texture2d.GetPixelBilinear(uv.x, uv.y);
                        OnGettedColorSample?.Invoke(color);
                    }
                }
            }
        }
    }
}


