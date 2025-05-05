using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private bool playOnStart;

        [SerializeField] private Instruction[] instructions;

        private void Start()
        {
            if (playOnStart)
            {
                Run();
            }
        }

        public async void Run()
        {
            await PrepareInstruction();

            foreach (Instruction instruction in instructions)
            {
                await instruction.Run();

                if (instruction.forceStopTrigger) break;
            }
        }

        private async Task PrepareInstruction()
        {
            foreach (Instruction instruction in instructions)
            {
                await instruction.ResetInstruction();
            }
        }
    }
}


