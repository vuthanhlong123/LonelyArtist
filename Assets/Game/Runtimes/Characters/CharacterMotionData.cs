using System;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [CreateAssetMenu(fileName = "Character Motion Data", menuName = "Game/Characters/Character Motion Data")]
    public class CharacterMotionData : ScriptableObject
    {
        [SerializeField] private string motionName;
        [SerializeField] private float moveSpeed;

        [Header("Anims")]
        [SerializeField] private AnimationData[] animationDatas;

        public string MotionName => motionName;
        public float MoveSpeed => moveSpeed;
        public AnimationData[] AnimationDatas => animationDatas;
    }

    [Serializable]
    public class AnimationData
    {
        public string name;
        public AnimationClip animation;
        public float threshold;
        public float speed;
    }
}
