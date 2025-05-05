using Game.Runtimes.Characters;
using Game.Runtimes.Commons;
using UnityEngine;

namespace Game.Runtimes.Interactions
{
    [RequireComponent (typeof(Rigidbody))]
    [RequireComponent (typeof(SphereCollider))]
    public class InteractionPoint : MonoBehaviour
    {
        [SerializeField] private LayerMask targetInteract;
        [SerializeField] private float range;
        [SerializeField] private GameObject interactionUI;
        [SerializeField] private bool cancelWhenTargetBusy;

        [Header("Actions")]
        [SerializeField] private Instruction[] interactActions;

        private Character currentTarget;

        private void Start()
        {
            interactionUI.SetActive(false);

            SphereCollider collider = GetComponent<SphereCollider>();
            if (collider)
            {
                collider.radius = range;
                collider.isTrigger = true;
            }

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(UnityExtension.ContainLayer(targetInteract, other.gameObject.layer))
            {
                currentTarget = other.gameObject.GetComponent<Character>();
                if (!currentTarget)
                {
                    currentTarget = other.GetComponentInParent<Character>();
                }

                if (currentTarget == null) return;

                currentTarget.CharacterBusyStateChanged += CurrentTarget_CharacterBusyStateChanged;

                if(cancelWhenTargetBusy)
                {
                    if (!currentTarget.IsBusy)
                        interactionUI.SetActive(true);
                }
                else
                {
                    interactionUI.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (UnityExtension.ContainLayer(targetInteract, other.gameObject.layer))
            {
                currentTarget = other.gameObject.GetComponent<Character>();
                if (!currentTarget)
                {
                    currentTarget = other.GetComponentInParent<Character>();
                }

                if (currentTarget == null) return;

                currentTarget.CharacterBusyStateChanged -= CurrentTarget_CharacterBusyStateChanged;

                interactionUI.SetActive(false);
            }
        }

        private void CurrentTarget_CharacterBusyStateChanged(bool arg0)
        {
            if (currentTarget.IsBusy && cancelWhenTargetBusy)
                interactionUI.SetActive(false);
        }

        public void Interacted()
        {
            interactionUI.SetActive(false);

            foreach (var action in interactActions)
            {
                action.Run();
            }
        }
    }
}

