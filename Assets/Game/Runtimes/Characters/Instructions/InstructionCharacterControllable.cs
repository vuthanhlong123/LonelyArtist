using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;
namespace Game.Runtimes.Characters
{
    public class InstructionCharacterControllable : Instruction
    {
        [SerializeField] private bool state;

        public override Task Run()
        {
            if (GameCharacterManager.Instance != null)
            {
                Character character = GameCharacterManager.Instance.GetMainCharacter();

                if (character == null) return Task.CompletedTask;

                character.IsControllable = state;
            }

            return Task.CompletedTask;
        }
    }
}


