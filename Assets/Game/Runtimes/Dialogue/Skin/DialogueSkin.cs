using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Runtimes.Dialogues
{
    public class DialogueSkin : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DialogueSkinContent actor1;
        [SerializeField] private DialogueSkinContent actor2;

        public event UnityAction submitNext;

        public void UpdateDialogueContent(DialoguePart dialoguePart)
        {
            DialogueContentData contentData = new DialogueContentData(dialoguePart.Content,
                                                                      dialoguePart.Actor.ActorName,
                                                                      dialoguePart.Actor.Avatar);

            if(dialoguePart.Actor.IsPlayer)
            {
                actor2.Show(false);
                actor1.ShowContent(contentData);
            }
            else
            {
                actor1.Show(false);
                actor2.ShowContent(contentData);
            }
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            submitNext?.Invoke();
        }
    }
}


