using UnityEngine;

namespace Game.Runtimes.Dialogues
{
    [CreateAssetMenu(fileName = "Dialogue Character Profile", menuName = "Game/Dialogue/Dialogue Character Profile")]
    public class DialogueCharacterProfile : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string actorName;
        [SerializeField] private Sprite avatar;
        [SerializeField] private bool isPlayer;

        public string ID => id;
        public string ActorName => actorName;
        public Sprite Avatar => avatar;
        public bool IsPlayer => isPlayer;
    }
}


