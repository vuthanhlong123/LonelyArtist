using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class InstructionRunTrigger : Instruction
    {
        [SerializeField] private Trigger trigger;

        public override Task Run()
        {
            if(trigger)
                trigger.Run();

            return Task.CompletedTask;
        }
    }
}


