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

        public void SavePaintingState(RenderTexture renderTexture)
        {
            Texture2D newTex = CustomRenderUtility.RenderTexturetoTexture2D(renderTexture);
            paintedTexture.Add(newTex);

            if(paintedTexture.Count > maxSaveState)
            {
                paintedTexture.RemoveAt(0);
            }
        }

        public void HandlingUndo(RenderTexture renderTexture)
        {
            if(paintedTexture.Count > 1)
            {
                Texture2D undoTex = paintedTexture[paintedTexture.Count - 2];
                CustomRenderUtility.Texture2DToRenderTexture(undoTex, renderTexture);
                paintedTexture.RemoveAt(paintedTexture.Count - 1);
            }
        }
    }
}


