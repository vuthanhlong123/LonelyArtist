using System.Threading.Tasks;
using Game.Runtimes.Commons;
using UnityEngine;

namespace Game.Runtimes.UIs.Common
{
    public class InstructionShowAcceptPopup : Instruction
    {
        [SerializeField] private string title;
        [SerializeField] private GameObject popupObjectSource;

        [SerializeField] private bool waitForCompleted;
        [SerializeField] private bool endTriggerOperationWithBoolean;

        private UIAcceptPopup m_Popup;
        public override async Task Run()
        {
            if(m_Popup == null)
            {
                GameObject obj = Instantiate(popupObjectSource);
                if (obj == null) return;

                UIAcceptPopup popup = obj.GetComponent<UIAcceptPopup>();
                if (popup == null) return;

                m_Popup = popup;
            }

            m_Popup.SetValue(title);
            m_Popup.Show(true);
            UIAcceptPopupOperator uiOperator = m_Popup.Operator;
            if (uiOperator == null) return;

            while(!uiOperator.finished)
            {
               await Task.Yield();
            }

            if (uiOperator.result == endTriggerOperationWithBoolean) ForceEndTriggerHandling();
        }
    }
}

