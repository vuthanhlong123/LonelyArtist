using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public class LAPaintControl_SavingUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button_Accept;
        [SerializeField] private Button button_Cancel;

        public event UnityAction<string> AcceptSavingEvent;

        private void Start()
        {
            button_Accept.onClick.AddListener(OnButtonAcceptClicked);
            button_Cancel.onClick.AddListener(OnButtonCancelClicked);
        }

        private void OnButtonAcceptClicked()
        {
            AcceptSavingEvent?.Invoke(inputField.text);
            //Show(false);
        }

        private void OnButtonCancelClicked()
        {
            Show(false);
        }

        public void Show(bool state)
        {
            inputField.text = "";

            gameObject.SetActive(state);
        }
    }
}

