using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Dialogues
{
    public class InstructionRunDialogue : Instruction
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private bool waitForCompleted;

        public override async Task Run()
        {
            if (!waitForCompleted)
            {
                dialogue.Run();
            }
            else
            {
                dialogue.Run();
                DialogueOperationHandler operation = dialogue.OperationHandler;

                if (operation == null) return;

                while (!operation.Finished)
                {
                    await Task.Yield();
                }
            }
        }
    }
}


