using System;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterMotion : CharacterKernel
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterInputData inputData;

        public float Speed => speed;

        public override void Update()
        {
            base.Update();
            UpdateMovementAnimation();
        }

        private void UpdateMovementAnimation()
        {
            animator.SetFloat("Movement", inputData.movement);
        }
    }
}


