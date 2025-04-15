using System;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterMotion : CharacterKernel
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
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
        }

        public void ChangeMotion(string motionName)
        {
            CharacterMotionData motionData = GetMotion(motionName);
            if (motionData != null)
            {
                speed = motionData.MoveSpeed;
                character.InputData.speed = motionData.MoveSpeed;
                character.InputData.currentMotion = motionData.MotionName;

                character.Animation.UpdateAnimation(motionData);
            }
        }

        public void ChangeMotion(int motionid)
        {
            CharacterMotionData motionData = GetMotion(motionid);
            if (motionData != null)
            {
                speed = motionData.MoveSpeed;
                character.InputData.speed = motionData.MoveSpeed;
                character.InputData.currentMotion = motionData.MotionName;
                character.Animation.UpdateAnimation(motionData);
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
    }
}


