using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintingManagerUI : MonoBehaviour
    {
        [SerializeField] private RectTransform brush;

        private void Update()
        {
            UpdateBrushPosition();
        }

        private void UpdateBrushPosition()
        {
            if (brush == null) return;

            Vector2 uiPosition = Input.mousePosition;
            uiPosition.x -= Screen.width / 2;
            uiPosition.y -= Screen.height / 2;
            brush.anchoredPosition = uiPosition;
        }
    }
}


