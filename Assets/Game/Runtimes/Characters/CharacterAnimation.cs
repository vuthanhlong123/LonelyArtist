using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }


    [Serializable]
    public class CharacterAnimation : CharacterKernel
    {
        [SerializeField] private Animator animator;

        private float weight;
        private float weightSpeed;

        private int layerId;

        public override void Update()
        {
            base.Update();
            UpdateMovementAnimation();
            UpdateTransitionToLayer();
        }

        private void UpdateMovementAnimation()
        {
            animator.SetFloat("Movement", character.InputData.movement);
        }

        private int GetLayerId(string layerName)
        {
            switch(layerName)
            {
                case "Run": return 0;
                case "Walk": return 1;
                case "Painting": return 2;
            }
            return 0;
        }

        public void UpdateAnimation(CharacterMotionData motionData)
        {
            ResetLayers();
            layerId = GetLayerId(motionData.MotionName);
            weightSpeed = motionData.TransitionSpeed;
            weight = 0;
            /*AnimatorController animatorController = new AnimatorController();
            animator.runtimeAnimatorController = animatorController;*/

            /* AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
             animator.runtimeAnimatorController = animatorOverrideController;

             var clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
             animatorOverrideController.GetOverrides(clipOverrides);

             clipOverrides["Idle"] = motionData.AnimationDatas[0].animation;
             clipOverrides["Walk"] = motionData.AnimationDatas[1].animation;
             clipOverrides["Run2"] = motionData.AnimationDatas[2].animation;

             animatorOverrideController.ApplyOverrides(clipOverrides);*/
        }

        private void UpdateTransitionToLayer()
        {
            if (animator.GetLayerWeight(layerId) >= 1) return;
            weight += Time.deltaTime * weightSpeed;
            animator.SetLayerWeight(layerId, weight);

            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerId);
            //animator.Play(stateInfo.fullPathHash, layerId, 0); // Set the current animation to 50% of
        }

        private void ResetLayers()
        {
            for (int i = 0; i< animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
        }
    }
}

