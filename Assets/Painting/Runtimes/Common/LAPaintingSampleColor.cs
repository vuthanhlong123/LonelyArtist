using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public class LAPaintingSampleColor : LAPaintingTool
    {
        [Space(15)]
        [SerializeField] private RectTransform brush_Trans;
        [SerializeField] private LayerMask targetLayer;

        public event UnityAction<Color> OnGettedColorSample;

        protected override void Start()
        {
            base.Start();
        }

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

                    if (renderer != null && renderer.material.mainTexture != null && meshCollider != null)
                    {
                        RenderTexture rendererTexture = renderer.material.mainTexture as RenderTexture;
                        Texture2D texture2d = toTexture2D(rendererTexture);
                        Vector2 uv = hit.textureCoord;
                        Color color = texture2d.GetPixelBilinear(uv.x, uv.y);
                        OnGettedColorSample?.Invoke(color);
                    }
                }
            }
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
    }
}


