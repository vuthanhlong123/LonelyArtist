using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class UnitCharacter : MonoBehaviour
    {
        [SerializeField] private bool isMainPlayer;

        public bool IsMainPlayer => isMainPlayer;

        protected virtual void Awake()
        {
            if(CharacterManager.Instance == null)
            {
                GameObject newObj = new GameObject("Character Manager");
                CharacterManager manager = newObj.AddComponent<CharacterManager>();
                manager.AddChild(this);
            }
            else
            {
                CharacterManager.Instance.AddChild(this);
            }
        }
    }
}


