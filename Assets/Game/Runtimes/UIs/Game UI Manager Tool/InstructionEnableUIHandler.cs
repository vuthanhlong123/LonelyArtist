using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.UIs
{
    public class InstructionEnableUIHandler : Instruction
    {
        [SerializeField] private bool state;

        [SerializeField] private string[] targets;

        public override Task Run()
        {
            if (targets == null) return Task.CompletedTask;

            if (GameUIManager.Instance == null) return Task.CompletedTask;

            GameUIManager.Instance.EnableUIs(targets, state);

            return Task.CompletedTask;
        }
    }
}


