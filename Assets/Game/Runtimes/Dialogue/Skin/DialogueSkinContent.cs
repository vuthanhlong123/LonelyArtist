using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtimes.Dialogues
{
    public class DialogueContentData
    {
        public string content;
        public string actorName;
        public Sprite actorAvatar;

        public DialogueContentData(string content, string actorName, Sprite actorAvatar)
        {
            this.content = content;
            this.actorName = actorName;
            this.actorAvatar = actorAvatar;
        }
    }

    public class DialogueSkinContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text_ConversationContent;
        [SerializeField] private TextMeshProUGUI text_ActorName;
        [SerializeField] private Image image_ActorAvatar;

        public void ShowContent(DialogueContentData data)
        {
            text_ConversationContent.text = data.content;
            text_ActorName.text = data.actorName;
            image_ActorAvatar.sprite = data.actorAvatar;

            Show(true);
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}


