using System;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [CreateAssetMenu(fileName = "Character Motion Data", menuName = "Game/Characters/Character Motion Data")]
    public class CharacterMotionData : ScriptableObject
    {
        [SerializeField] private string motionName;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool useBlendTree = true;

        [Header("Anims")]
        [SerializeField] private AnimationData[] animationDatas;

        [SerializeField] private float transitionSpeed;

        public string MotionName => motionName;
        public float MoveSpeed => moveSpeed;
        public bool UseBlendTree => useBlendTree;

        public AnimationData[] AnimationDatas => animationDatas;
        public float TransitionSpeed => transitionSpeed;
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
