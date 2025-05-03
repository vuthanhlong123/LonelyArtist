using UnityEngine;

namespace LA.Common.Tools
{
    public class CustomRenderUtility : MonoBehaviour
    {
        public static Texture2D RenderTexturetoTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }

        public static void Texture2DToRenderTexture(Texture2D texture2D, RenderTexture renderTexture)
        {
            // Set the active RenderTexture
            RenderTexture.active = renderTexture;

            // Copy the Texture2D to the RenderTexture
            Graphics.Blit(texture2D, renderTexture);

            // Reset the active RenderTexture
            RenderTexture.active = null;
        }

        public static Texture2D SpriteToTexture2D(Sprite sprite)
        {
            Texture2D originalTexture = sprite.texture;

            // Step 2 (Optional): Create a copy of the Texture2D
            Texture2D copiedTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, originalTexture.mipmapCount > 1);
            copiedTexture.SetPixels(originalTexture.GetPixels());
            copiedTexture.Apply();

            return copiedTexture;
        }
    }
}


