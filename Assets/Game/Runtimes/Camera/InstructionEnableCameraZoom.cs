using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Cameras
{
    public class InstructionEnableCameraZoom : Instruction
    {
        [SerializeField] private CameraInputData inputData;
        [SerializeField] private bool value;

        public override Task Run()
        {
            if(inputData)
            {
                inputData.isZoomAvailable = value;
            }
            return Task.CompletedTask;
        }

    }
}


