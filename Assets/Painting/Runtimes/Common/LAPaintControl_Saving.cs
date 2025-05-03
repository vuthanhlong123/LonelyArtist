using System.IO;
using Game.Runtimes.Commons;
using LA.Common.Tools;
using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintControl_Saving : MonoBehaviour
    {
        [SerializeField] private LAPaintingManager paintManager;
        [SerializeField] private LAPaintControl_SavingUI ui;
        [SerializeField] private LAPaintControl_SavingStatusPopup statusPopup;

        public void Execute()
        {
            ui.Show(true);
        }

        private void OnEnable()
        {
            ui.AcceptSavingEvent += Ui_AcceptSavingEvent;
        }

        private void Ui_AcceptSavingEvent(string saveName)
        {
            HandlingSave(saveName);
        }

        private void HandlingSave(string saveName)
        {
            bool result = SaveTexture(saveName);
            if(result)
            {
                ui.Show(false);
            }
        }

        public bool SaveTexture(string fileName)
        {
            Texture2D savedTexture = CustomRenderUtility.RenderTexturetoTexture2D(paintManager.GetRenderTex);

            // Lưu Texture thành PNG
            byte[] bytes = savedTexture.EncodeToPNG();
            string saveFolderPath = UnityExtension.SavePaintFolderPath; 
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            string saveFilePath = saveFolderPath + $"/{fileName}.png";
            if (File.Exists(saveFilePath))
            {
                statusPopup.Show("File already exists");
                return false;
            }

            System.IO.File.WriteAllBytes(saveFilePath, bytes);

            Debug.Log("Texture saved at: " + saveFilePath);
            statusPopup.Show("Save successful");
            return true;
        }

        private void OnDisable()
        {
            ui.AcceptSavingEvent -= Ui_AcceptSavingEvent;
        }
    }
}

