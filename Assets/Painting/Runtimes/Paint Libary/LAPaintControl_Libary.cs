using Game.Runtimes.Commons;
using Game.Runtimes.UIs.Common;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace LA.Painting.PaintLibary
{
    public class LAPaintControl_Libary : MonoBehaviour
    {
        [SerializeField] private LAPaintLibaryUI libaryUI;
        [SerializeField] private GameObject acceptPopupPrefab;

        private string[] files;

        private UIAcceptPopup uiAcceptPopup;

        public void Execute()
        {
            string textureSavedPath = UnityExtension.SavePaintFolderPath;

            files = Directory.GetFiles(textureSavedPath);

            libaryUI.ClearLibaryUI();
            libaryUI.Show(libaryUI);

            LoadPaints(files);
        }

        private void LoadPaints(string[] files)
        {
            foreach (string file in files)
            {
                LoadPaint(file);
            }
        }

        private async void LoadPaint(string path)
        {
            await Task.Yield();

            if (!File.Exists(path)) return;

            byte[] bytes = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(1024, 1024);

            if(texture.LoadImage(bytes))
            {
                libaryUI.CreateTextureLibary(texture);
            }
        }

        public async void DropTexture(int id)
        {
            if(uiAcceptPopup == null)
            {
                GameObject obj = Instantiate(acceptPopupPrefab);

                uiAcceptPopup = obj.GetComponent<UIAcceptPopup>();
            }

            if (uiAcceptPopup == null) return;

            uiAcceptPopup.SetValue("Do you want drop this paint");
            uiAcceptPopup.Show(true);
            var operation = uiAcceptPopup.Operator;

            if (operation == null) return;
 
            while(!operation.finished)
            {
                await Task.Yield();
            }

            if(operation.result)
            {
                string path = files[id];
                if (!File.Exists(path)) return;

                File.Delete(path);

                Execute();
            }
        }
    }
}


