using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LA.Painting
{
    public class LAPaintingSVImageController : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        [SerializeField] private Image pickerImage;

        [SerializeField] private RawImage SVImage;

        [SerializeField] private LAPaintingColorPicker colorPicker;

        private RectTransform rectTrans;
        private RectTransform pickerTrans;

        private void Awake()
        {
            rectTrans = GetComponent<RectTransform>();
            
            pickerTrans = pickerImage.GetComponent<RectTransform>();
            pickerTrans.position = new Vector2(-(rectTrans.sizeDelta.x * 0.5f), -(rectTrans.sizeDelta.y * 0.5f));
        }

        private void UpdateColor(PointerEventData eventData)
        {
            Vector3 pos = rectTrans.InverseTransformPoint(eventData.position);

            float deltaX = rectTrans.sizeDelta.x * 0.5f;
            float deltaY = rectTrans.sizeDelta.y * 0.5f;

            /*if (pos.x < -deltaX)
            {
                pos.x = -deltaX;
            }
            else if (pos.x > deltaX)
            {
                pos.x = deltaX;
            }
            
            if(pos.y < -deltaY)
            {
                pos.y = -deltaY;
            }
            else if(pos.y > deltaY)
            {
                pos.y = deltaY;
            }*/

            pos.x = Mathf.Clamp(pos.x, -deltaX, deltaX);
            pos.y = Mathf.Clamp(pos.y, -deltaY, deltaY);


            float x = pos.x + deltaX;
            float y = pos.y + deltaY;

            float xNorm = x / rectTrans.sizeDelta.x;
            float yNorm = y / rectTrans.sizeDelta.y;

            pickerTrans.localPosition = pos;
            pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);

            colorPicker.SetSV(xNorm, yNorm);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }
    }
}


