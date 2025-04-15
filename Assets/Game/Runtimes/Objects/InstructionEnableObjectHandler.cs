using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Objects
{
    public class InstructionEnableObjectHandler : Instruction
    {
        [SerializeField] private string[] targets;
        [SerializeField] private bool state;

        public override Task Run()
        {
            if (targets == null) return Task.CompletedTask;

            if (GameObjectManager.Instance == null) return Task.CompletedTask;

            GameObjectManager.Instance.EnableObjects(targets, state);

            return Task.CompletedTask;
        }
    }
}


