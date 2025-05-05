using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtimes.Characters
{
    public class Character : UnitCharacter
    {
        [SerializeField] private bool isControllable;
        [SerializeField] private bool isBusy;

        [Header("Memebers")]
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private CharacterDriver driver;
        [SerializeField] private CharacterAnimation _animation;

        [SerializeField] private CharacterInputData _inputData;

        public bool IsControllable { get { return isControllable; } set { isControllable = value;  } }

        public bool IsBusy { get { return isBusy; } set { isBusy = value; CharacterBusyStateChanged?.Invoke(isBusy); } }

        public CharacterMotion Motion => motion;
        public CharacterDriver Driver => driver;
        public CharacterAnimation Animation => _animation;

        public CharacterInputData InputData => _inputData;

        //Event
        public event UnityAction<bool> CharacterBusyStateChanged;

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


