using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Runtimes.Menu
{
    public class PointerEnterColorChnage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image target;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color changingColor;

        public void OnPointerEnter(PointerEventData eventData)
        {
            target.color = changingColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            target.color = normalColor;
        }
    }
}


