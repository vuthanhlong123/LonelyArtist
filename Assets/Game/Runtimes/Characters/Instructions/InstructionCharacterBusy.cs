using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class InstructionCharacterBusy : Instruction
    {
        [SerializeField] private Character targetCharacter;
        [SerializeField] private bool targetIsPlayer;

        [Header("Value")]
        [SerializeField] private bool value;

        public override Task Run()
        {
            if(targetIsPlayer)
            {
                Character player = GameCharacterManager.Instance.GetMainCharacter();
                if (player)
                {
                    player.IsBusy = value;
                }

                return Task.CompletedTask;
            }

            if (targetCharacter)
            {
                targetCharacter.IsBusy = value;
            }

            return Task.CompletedTask;
        }
    }
}


