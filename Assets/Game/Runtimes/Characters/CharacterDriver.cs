using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterDriver : CharacterKernel
    {
        [SerializeField] private NavMeshAgent agent;

        private Vector3 moveDirection;
        private Transform cameraTrans;

        public override void Start()
        {
            base.Start();

            cameraTrans = Camera.main.transform;
        }

        public override void Update()
        {
            base.Update();
            UpdateProperties();
            HandlingMovement();
        }

        private void UpdateProperties()
        {
            if(agent)
            {
                agent.speed = character.Motion.Speed;
            }
        }

        private void HandlingMovement()
        {
            if (!character.IsControllable) return;

            HandlingMoveToDirection();
        }

        private void HandlingMoveToDirection()
        {
            if (character.InputData.moveInput.magnitude > 0)
            {
                Vector2 inputVector = character.InputData.moveInput;

                moveDirection = character.gameObject.transform.position - cameraTrans.position;
                moveDirection.y = 0;

                moveDirection = moveDirection.normalized;

                Vector3 movemonetRight = Vector3.Cross(moveDirection, Vector3.up);
                Vector3 movementVector = moveDirection* inputVector.y + movemonetRight * -inputVector.x;
                if(movementVector.magnitude>1) movementVector = movementVector.normalized;
                character.InputData.moveVelocity = movementVector;
                agent.velocity = movementVector * character.Motion.Speed;
            }
        }
    }
}


