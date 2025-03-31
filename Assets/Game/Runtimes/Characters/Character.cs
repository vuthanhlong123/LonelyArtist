using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class Character : MonoBehaviour
    {
        [Header("Memebers")]
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private CharacterDriver driver;

        public CharacterMotion Motion => motion;
        public CharacterDriver Driver => driver;

        private void Awake()
        {
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


