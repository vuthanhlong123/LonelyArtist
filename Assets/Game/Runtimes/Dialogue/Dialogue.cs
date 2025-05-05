using System;
using UnityEngine;

namespace Game.Runtimes.Dialogues
{
    public class DialogueOperationHandler
    {
        public bool Finished;

        public DialogueOperationHandler() 
        {
            Finished = false;
        }
    }

    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueSkin;
        [SerializeField] private DialoguePart[] conversations;

        private DialogueSkin dialogueSkinComponent;
        private int currentConversation;

        private DialogueOperationHandler operationHandler = new DialogueOperationHandler();

        public DialogueOperationHandler OperationHandler => operationHandler;

        public void Run()
        {
            operationHandler.Finished = false;

            if (dialogueSkinComponent == null)
            {
                GameObject newObj = Instantiate(dialogueSkin);
                dialogueSkinComponent = newObj.GetComponent<DialogueSkin>();

                if (dialogueSkinComponent == null) return;

            }

            dialogueSkinComponent.submitNext += DialogueSkinComponent_submitNext;
            dialogueSkinComponent.Show(true);

            currentConversation = 0;
            dialogueSkinComponent.UpdateDialogueContent(conversations[currentConversation]);
        }

        private void DialogueSkinComponent_submitNext()
        {
            currentConversation++;
            if (currentConversation >= conversations.Length)
            {
                EndConversation();
                return;
            }

            dialogueSkinComponent.UpdateDialogueContent(conversations[currentConversation]);
        }

        private void EndConversation()
        {
            dialogueSkinComponent.submitNext -= DialogueSkinComponent_submitNext;
            dialogueSkinComponent.Show(false);
            operationHandler.Finished = true;
        }
    }

    [Serializable]
    public class DialoguePart
    {
        [SerializeField] private DialogueCharacterProfile actor;
        [SerializeField] private string content;

        public string Content => content;
        public DialogueCharacterProfile Actor => actor;
    }
}


