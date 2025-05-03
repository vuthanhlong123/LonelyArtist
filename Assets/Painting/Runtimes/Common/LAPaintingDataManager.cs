using LA.Common.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintingDataManager : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private int maxSaveState;

        [SerializeField] private List<Texture2D> paintedTexture = new List<Texture2D>();

        private RenderTexture renderTexture;

        public void StarUp(RenderTexture renderTexture)
        {
            this.renderTexture = renderTexture;
        }

        public void SavePaintingState()
        {
            Texture2D newTex = CustomRenderUtility.RenderTexturetoTexture2D(renderTexture);
            paintedTexture.Add(newTex);

            if(paintedTexture.Count > maxSaveState)
            {
                paintedTexture.RemoveAt(0);
            }
        }

        public void HandlingUndo()
        {
            if(paintedTexture.Count > 1)
            {
                Texture2D undoTex = paintedTexture[paintedTexture.Count - 2];
                CustomRenderUtility.Texture2DToRenderTexture(undoTex, renderTexture);
                paintedTexture.RemoveAt(paintedTexture.Count - 1);
            }
        }

        public void ResetPaintData()
        {
            paintedTexture.Clear();
        }

        public int PaintedStateCount => paintedTexture.Count;

       /* public void SaveTexture(int index)
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
        }*/
    }
}


