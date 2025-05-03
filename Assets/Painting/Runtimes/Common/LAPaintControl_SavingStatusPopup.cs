using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public class LAPaintControl_SavingStatusPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text_Content;
        [SerializeField] private Image image_PopupFrame;

        [Header("Show Animation")]
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve opacityCurve;
        [SerializeField] private Color contentColor;
        [SerializeField] private Color frameColor;

        private float durationCount;

        private void Update()
        {
            ShowAnim();
        }

        private void ShowAnim()
        {
            durationCount += Time.deltaTime;
            float colorAlpha = opacityCurve.Evaluate(durationCount / duration);

            contentColor.a = colorAlpha;
            frameColor.a = colorAlpha;

            text_Content.color = contentColor;
            image_PopupFrame.color = frameColor;

            if(durationCount >= duration)
            {
                gameObject.SetActive(false);
            }
        }

        public void Show(string content)
        {
            durationCount = 0;
            text_Content.text = content;

            float colorAlpha = opacityCurve.Evaluate(durationCount / duration);

            contentColor.a = colorAlpha;
            frameColor.a = colorAlpha;

            text_Content.color = contentColor;
            image_PopupFrame.color = frameColor;

            gameObject.SetActive(true);
        }
    }

}

