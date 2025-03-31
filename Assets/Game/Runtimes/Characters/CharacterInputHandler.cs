using UnityEngine;

namespace Game.Runtimes.Characters
{
    public class CharacterInputHandler : MonoBehaviour
    {
        [SerializeField] private CharacterInputData characterInputData;

        [Space(10)]
        [SerializeField] private FixedJoystick joystick;

        private Vector2 finalInput;

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


