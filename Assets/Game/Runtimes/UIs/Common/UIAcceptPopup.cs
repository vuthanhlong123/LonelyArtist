using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Runtimes.UIs.Common
{
    public class UIAcceptPopupOperator
    {
        public bool result;
        public bool finished;

        public UIAcceptPopupOperator()
        {
            finished = false;
            result = false;
        }
    }

    public class UIAcceptPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text_title;
        [SerializeField] private Button button_Yes;
        [SerializeField] private Button button_No;

        public event UnityAction<bool> PopupResult;

        public UIAcceptPopupOperator Operator = new UIAcceptPopupOperator();

        private void Start()
        {
            button_Yes.onClick.AddListener(OnYesButtonClicked);
            button_No.onClick.AddListener(OnNoButtonClicked);
        }

        private void OnYesButtonClicked()
        {
            Operator.result = true;
            Operator.finished = true;
            PopupResult?.Invoke(true);
            Show(false);
        }

        private void OnNoButtonClicked()
        {
            Operator.result = false;
            Operator.finished = true;
            PopupResult?.Invoke(false);
            Show(false);
        }
        public void SetValue(string title)
        {
            text_title.text = title;

            if(Operator != null)
            {
                Operator.finished = false;
            }
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}

