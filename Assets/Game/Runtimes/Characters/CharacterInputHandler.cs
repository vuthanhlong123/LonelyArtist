using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtimes.Characters
{
    public class CharacterInputHandler : MonoBehaviour
    {
        [SerializeField] private CharacterInputData characterInputData;

        [Space(10)]
        [SerializeField] private FixedJoystick joystick;

        [Header("Motions")]
        [SerializeField] private Button button_Run;
        [SerializeField] private Button button_Walk;

        private Vector2 finalInput;

        private void Start()
        {
            AddListener();
        }

        private void AddListener()
        {
            button_Run.onClick.AddListener(OnButtonRunClicked);
            button_Walk.onClick.AddListener(OnButtonWalkClicked);
        }

        private void OnButtonRunClicked()
        {
            Character mainCharacter = GameCharacterManager.Instance.GetMainCharacter();
            if(mainCharacter)
            {
                mainCharacter.Motion.ChangeMotion("Run");
            }
        }

        private void OnButtonWalkClicked()
        {
            Character mainCharacter = GameCharacterManager.Instance.GetMainCharacter();
            if (mainCharacter)
            {
                mainCharacter.Motion.ChangeMotion("Walk");
            }
        }

        private void Update()
        {
            if(joystick)
                finalInput = joystick.Direction;

            HandlingNumpadInput();

            characterInputData.moveInput = finalInput;
        }

        private void HandlingNumpadInput()
        {
            if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") !=0)
            {
                finalInput.x = Input.GetAxis("Horizontal");
                finalInput.y = Input.GetAxis("Vertical");
            }
        }
    }
}


