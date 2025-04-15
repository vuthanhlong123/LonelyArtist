using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class InstructionCharacterChangeState : Instruction
    {
        [SerializeField] private CharacterSelectType type;
        [SerializeField] private Character character;
        [SerializeField] private string state;

        public override Task Run()
        {
            if (GameCharacterManager.Instance == null) return Task.CompletedTask;

            if(type.Equals (CharacterSelectType.MainCharacter))
            {
                character = GameCharacterManager.Instance.GetMainCharacter();
            }
            
            if(character == null) return Task.CompletedTask;

            character.Motion.ChangeMotion(state);

            return Task.CompletedTask;
        }
    }
}

