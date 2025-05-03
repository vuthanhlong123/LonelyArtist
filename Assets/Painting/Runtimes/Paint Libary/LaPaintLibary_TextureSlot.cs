using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LA.Painting.PaintLibary
{

    public class LaPaintLibary_TextureSlot : MonoBehaviour
    {
        public int ID;
        [SerializeField] private RawImage image;
        [SerializeField] private RectTransform rect_Container;
        [SerializeField] private float stepValue;
        [SerializeField] private float targetScale;

        [SerializeField] private Button button_Drop;
        [SerializeField] private float startShowAtLinear;

        public static event UnityAction<int> SubmitDropPaint;

        private void Start()
        {
            button_Drop.onClick.AddListener(OnButtonDropClicked);
        }

        private void OnButtonDropClicked()
        {
            SubmitDropPaint?.Invoke(ID);
        }

        public void SetValue(Texture2D texture, int id)
        {
            ID = id;

            if(image != null) 
                image.texture = texture;
        }

        public void UpdateSize(RectTransform parent)
        {
            if (rect_Container == null) return;

            float parentPosX = -parent.anchoredPosition.x;

            if (parentPosX <= ID*stepValue && ID * parentPosX >= ID*stepValue-stepValue)
            {
                parentPosX -= stepValue * (ID - 1);
                rect_Container.localScale = Vector3.Lerp(Vector3.one, new Vector3(targetScale, targetScale, targetScale), parentPosX / stepValue);
                UpdateButtonDropDisplament(parentPosX/stepValue);
            }
            else if(parentPosX > ID * stepValue && parentPosX < ID*stepValue + stepValue)
            {
                parentPosX -= stepValue * (ID - 1);
                parentPosX = stepValue*2 - parentPosX;
                rect_Container.localScale = Vector3.Lerp(Vector3.one, new Vector3(targetScale, targetScale), parentPosX / stepValue);
                UpdateButtonDropDisplament(parentPosX / stepValue);
            }
        }

        private void UpdateButtonDropDisplament(float linear)
        {
            if(linear >= startShowAtLinear)
            {
                if (!button_Drop.gameObject.activeSelf)
                {
                    button_Drop.gameObject.SetActive(true);
                }
            }
            else
            {
                if (button_Drop.gameObject.activeSelf)
                {
                    button_Drop.gameObject.SetActive(false);
                }
            }

        }
    }
}
