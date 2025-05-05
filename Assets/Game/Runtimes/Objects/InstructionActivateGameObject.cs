using Game.Runtimes.Commons;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Objects
{
    public class InstructionActivateGameObject : Instruction
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool value;

        public override Task Run()
        {
            if(_gameObject)
            {
                _gameObject.SetActive(value);
            }

            return Task.CompletedTask;
        }

    }
}


