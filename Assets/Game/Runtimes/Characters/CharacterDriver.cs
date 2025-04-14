using NUnit.Framework.Internal;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterDriver : CharacterKernel
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private CharacterInputData inputData;
        [SerializeField] private Transform test;

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
            HandlingMoveToDirection();
        }

        private void HandlingMoveToDirection()
        {
            if (inputData.moveInput.magnitude > 0)
            {
                Vector2 inputVector = inputData.moveInput;

                moveDirection = character.gameObject.transform.position - cameraTrans.position;
                moveDirection.y = 0;

                test.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                moveDirection = moveDirection.normalized;

                Vector3 movemonetRight = Vector3.Cross(moveDirection, Vector3.up);
                Vector3 movementVector = moveDirection* inputVector.y + movemonetRight * -inputVector.x;
                if(movementVector.magnitude>1) movementVector = movementVector.normalized;
                agent.velocity = movementVector * character.Motion.Speed;
            }
        }
    }
}


