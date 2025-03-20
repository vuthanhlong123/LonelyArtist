using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace LA.Painting
{
    public class LAPaintingManager : MonoBehaviour
    {
        public Material paintMaterial; // Material sử dụng Shader vẽ
        public Texture2D baseTexture;  // Texture gốc
        public Texture2D brushTex;
        public RenderTexture renderTexturePreview; // RenderTexture lưu trạng thái

        private RenderTexture renderTexture;
        private int loop;

        private Vector2 currentPos;

        void Start()
        {
            // Tạo RenderTexture để lưu kết quả
            renderTexture = new RenderTexture(1024, 1024, 0);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();

            // Copy Texture gốc vào RenderTexture
            Graphics.Blit(baseTexture, renderTexture);

            // Gán RenderTexture vào Material
            paintMaterial.SetTexture("_mainTex", renderTexture);
        }

        void Update()
        {
            if (Input.GetMouseButton(0)) // Nhấp chuột trái
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (currentPos == hit.textureCoord) return;
                    currentPos = hit.textureCoord;
                    paintMaterial.SetVector("_BrushPosition", hit.textureCoord);

                    // Thực hiện vẽ lên RenderTexture
                    Graphics.Blit(null, renderTexturePreview, paintMaterial);
                    Graphics.Blit(renderTexturePreview, renderTexture);

                    //Texture2D texn = toTexture2D(renderTexture);
                    //ConvertTexture2DToRenderTexture(texn, renderTexture);
                    loop++;
                    //SaveTexture(loop);
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
    }
}


