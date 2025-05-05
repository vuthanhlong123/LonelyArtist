using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.System
{
    public class InstructionExitGame : Instruction
    {
        public override Task Run()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

            return Task.CompletedTask;
        }
    }
}


