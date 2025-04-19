using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private Instruction[] instructions;

        public async void Run()
        {
            foreach (Instruction instruction in instructions)
            {
                await instruction.Run();
            }
        }
    }
}


