using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class UnitCharacter : MonoBehaviour
    {
        [SerializeField] private bool isMainPlayer;

        public bool IsMainPlayer => isMainPlayer;

        protected virtual void Awake()
        {
            if(GameCharacterManager.Instance == null)
            {
                GameObject newObj = new GameObject("Character Manager");
                GameCharacterManager manager = newObj.AddComponent<GameCharacterManager>();
                manager.AddChild(this);
            }
            else
            {
                GameCharacterManager.Instance.AddChild(this);
            }
        }
    }
}


