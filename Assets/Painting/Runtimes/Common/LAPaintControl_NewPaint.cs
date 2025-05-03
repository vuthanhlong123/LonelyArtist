using System.Threading.Tasks;
using Game.Runtimes.UIs.Common;
using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintControl_NewPaint : MonoBehaviour
    {
        [SerializeField] private LAPaintingManager paintManager;
        [SerializeField] private LAPaintingDataManager paintDataManager;
        [SerializeField] private GameObject acceptObjSource;

        private UIAcceptPopup uiAcceptPopup;
        public async Task Execute()
        {
            if(paintDataManager.PaintedStateCount > 1)
            {
                if(uiAcceptPopup == null)
                {
                    GameObject obj = Instantiate(acceptObjSource);
                    if (obj == null) return;

                    uiAcceptPopup = obj.GetComponent<UIAcceptPopup>();
                    if (uiAcceptPopup == null) return;
                }

                uiAcceptPopup.SetValue("Do you want make a new paint");
                var operation = uiAcceptPopup.Operator;
                if (operation == null) return;

                uiAcceptPopup.Show(true);

                while (!operation.finished)
                {
                    await Task.Yield();
                }

                if (operation.result)
                {
                    paintManager.NewPaint();
                }

                return;
            }

            paintManager.NewPaint();
        }
    }
}

