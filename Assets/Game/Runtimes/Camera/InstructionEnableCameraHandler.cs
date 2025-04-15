using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Cameras
{
    public class InstructionEnableCameraHandler : Instruction
    {
        [SerializeField] private string[] targets;
        [SerializeField] private bool state;

        public override Task Run()
        {
            if (targets == null) return Task.CompletedTask;
            if (GameCameraManager.Instance == null) return Task.CompletedTask;
            GameCameraManager.Instance.EnableCameras(targets, state);

            return Task.CompletedTask;
        }
    }
}


