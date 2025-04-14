using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class Character : UnitCharacter
    {
        [Header("Memebers")]
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private CharacterDriver driver;

        public CharacterMotion Motion => motion;
        public CharacterDriver Driver => driver;

        protected override void Awake()
        {
            base.Awake();
            StartUp();
            motion.Awake();
            driver.Awake();
        }

        private void StartUp()
        {
            motion.StartUp(this);
            driver.StartUp(this);
        }

        private void Start()
        {
            motion.Start();
            driver.Start();
        }

        private void Init()
        {

        }

        private void Update()
        {
            motion.Update();
            driver.Update();
        }

        
    }
}


