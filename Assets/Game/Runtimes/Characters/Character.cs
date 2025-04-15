using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class Character : UnitCharacter
    {
        [SerializeField] private bool isControllable;

        [Header("Memebers")]
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private CharacterDriver driver;
        [SerializeField] private CharacterAnimation _animation;

        [SerializeField] private CharacterInputData _inputData;

        public bool IsControllable { get { return isControllable; } set { isControllable = value; } }

        public CharacterMotion Motion => motion;
        public CharacterDriver Driver => driver;
        public CharacterAnimation Animation => _animation;

        public CharacterInputData InputData => _inputData;

        protected override void Awake()
        {
            base.Awake();
            StartUp();
            motion.Awake();
            driver.Awake();
            Animation.Awake();
        }

        private void StartUp()
        {
            motion.StartUp(this);
            driver.StartUp(this);
            Animation.StartUp(this);
        }

        private void Start()
        {
            motion.Start();
            driver.Start();
            Animation.Start();
        }

        private void Init()
        {

        }

        private void Update()
        {
            motion.Update();
            driver.Update();
            Animation.Update();
        }


    }
}


