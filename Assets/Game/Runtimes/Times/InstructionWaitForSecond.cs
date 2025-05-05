using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Times
{
    public class InstructionWaitForSecond : Instruction
    {
        [Tooltip("Time to wait (second)")]
        [SerializeField] private float time; // second

        public override async Task Run()
        {
            await Task.Delay((int)(time*1000));

            //return Task.CompletedTask;
        }
    }
}


