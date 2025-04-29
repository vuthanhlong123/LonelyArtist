using Game.Runtimes.Commons;
using UnityEngine;

namespace Game.Runtimes.Objects
{
    public class ActivateObjectByCollide : Instruction
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private GameObject targetObject;
        [SerializeField] private bool value;
        
        private void OnTriggerEnter(Collider other)
        {
            if(UnityExtension.ContainLayer(targetLayer, other.gameObject.layer))
            {
                targetObject.SetActive(value);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (UnityExtension.ContainLayer(targetLayer, other.gameObject.layer))
            {
                targetObject.SetActive(!value);
            }
        }
    }
}

