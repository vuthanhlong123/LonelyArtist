using UnityEngine;

namespace Game.Runtimes.Characters
{
    [CreateAssetMenu(fileName = "Character Input Data", menuName = "Game/Characters/Character Input Data")]
    public class CharacterInputData : ScriptableObject
    {
        public float speed;
        public CharacterMotionType motion;
        public string currentMotion;
        public Vector2 moveInput;
        public Vector3 moveVelocity;
        public float movement => moveVelocity.magnitude;
    }
}


