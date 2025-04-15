using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class GameCharacterManager : MonoBehaviour
    {
        public static GameCharacterManager Instance;

        private List<UnitCharacter> characters = new List<UnitCharacter>();
        private UnitCharacter mainCharacter;

        private void Awake()
        {
            Instance = this;
        }

        public void AddChild(UnitCharacter character)
        {
            characters.Add(character);
            if(character.IsMainPlayer) mainCharacter = character;
        }

        public Character GetMainCharacter()
        {
            return (Character)mainCharacter ;
        }
    }
}


