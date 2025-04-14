using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterMotion : CharacterKernel
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterInputData inputData;
        [SerializeField] private CharacterMotionData[] motionData;

        public float Speed => speed;

        public override void Start()
        {
            InitCharacterMotion();
        }

        private void InitCharacterMotion()
        {
            ChangeMotion(0);
        }

        public override void Update()
        {
            base.Update();
            UpdateMovementAnimation();
        }

        private void UpdateMovementAnimation()
        {
            animator.SetFloat("Movement", inputData.movement);
        }

        public void ChangeMotion(string motionName)
        {
            CharacterMotionData motionData = GetMotion(motionName);
            if (motionData != null)
            {
                speed = motionData.MoveSpeed;
                inputData.speed = motionData.MoveSpeed;
                inputData.currentMotion = motionData.MotionName;

                CreateNewMachineLayer(motionData);
            }
        }

        public void ChangeMotion(int motionid)
        {
            CharacterMotionData motionData = GetMotion(motionid);
            if (motionData != null)
            {
                speed = motionData.MoveSpeed;
                inputData.speed = motionData.MoveSpeed;
                inputData.currentMotion = motionData.MotionName;
                CreateNewMachineLayer(motionData);
            }
        }

        private CharacterMotionData GetMotion(string motionName)
        {
            foreach (var motion in motionData)
            {
                if(motion.MotionName == motionName) return motion;
            }

            return null;
        }

        private CharacterMotionData GetMotion(int id)
        {
            if(id<0 || id>=motionData.Length) return null;

            return motionData[id];
        }

        private void CreateNewMachineLayer(CharacterMotionData motionData)
        {

            AnimatorController animatorController = new AnimatorController();
            animator.runtimeAnimatorController = animatorController;

            animatorController.AddParameter("Movement", AnimatorControllerParameterType.Float);

            // Create a new layer
            AnimatorControllerLayer layer = new AnimatorControllerLayer
            {
                name = $"{motionData.MotionName} Layer",
                stateMachine = new AnimatorStateMachine()
            };
            animatorController.AddLayer(layer);

            // Create a Blend Tree
            AnimatorState blendTreeState = layer.stateMachine.AddState("Blend Tree");
            BlendTree blendTree;
            blendTreeState.motion = blendTree = new BlendTree
            {
                name = "Blend Tree",
                blendType = BlendTreeType.Simple1D,
                blendParameter = "Movement"
            };

            // Add animations to the Blend Tree
            AnimationData[] animationDatas = motionData.AnimationDatas;
            foreach (var animationData in animationDatas)
            {
                blendTree.AddChild(animationData.animation, animationData.threshold);
            }

            ChildMotion[] childMotion = blendTree.children;
            for (int i = 0; i < childMotion.Length; i++)
            {
                childMotion[i].timeScale = animationDatas[i].speed;
            }
            blendTree.children = childMotion;
        }
    }
}


