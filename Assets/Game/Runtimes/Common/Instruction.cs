using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class Instruction : MonoBehaviour
    {
        public bool forceStopTrigger { get; private set; }

        public Task ResetInstruction()
        {
            forceStopTrigger = false;

            return Task.CompletedTask;
        }

        public virtual Task Run()
        {
            return Task.CompletedTask;
        }

        public void ForceEndTriggerHandling()
        {
            forceStopTrigger = true;
        }
    }
}


