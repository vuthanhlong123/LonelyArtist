using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Runtimes.Cameras
{
    public class InstructionCameraBlur : Instruction
    {
        [SerializeField] private VolumeProfile volumeProfile;
        [SerializeField] private bool value;

        public override Task Run()
        {
            var volumeComponents = volumeProfile.components;

            foreach (var component in volumeComponents)
            {
                if(component.name == "DepthOfField")
                    component.active = value;
            }
            return Task.CompletedTask;
        }
    }
}


