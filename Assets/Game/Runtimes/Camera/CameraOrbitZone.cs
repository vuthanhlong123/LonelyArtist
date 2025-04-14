using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Runtimes.Cameras
{
    public class CameraOrbitZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CameraInputData inputData;

        private Vector3 pointerDownPos;

        public void OnPointerDown(PointerEventData eventData)
        {
            inputData.isChange = false;
            pointerDownPos = Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 error = Input.mousePosition - pointerDownPos;
            inputData.axisValue.x = error.x;
            inputData.axisValue.y = error.y;

            pointerDownPos = Input.mousePosition;
            inputData.isChange = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inputData.isChange = false;
            pointerDownPos = Vector3.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}


