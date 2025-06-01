using Game.Runtimes.Characters;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Runtimes.Cameras
{
    public class GameCameraController : MonoBehaviour
    {
        [SerializeField] private CameraInputData inputData;
        [SerializeField] private CinemachineOrbitalFollow orbitCamera;

        private void Update()
        {
            UpdateCameraOrbitAxis();
            UpdateCameraDistance();
        }

        private void UpdateCameraOrbitAxis()
        {
            if (!inputData.isChange) return;

            var horizontal = orbitCamera.HorizontalAxis;
            horizontal.Value += inputData.orbitX * Time.deltaTime;
            orbitCamera.HorizontalAxis = horizontal;

            var vertical = orbitCamera.VerticalAxis;
            vertical.Value += -inputData.orbitY * Time.deltaTime;
            orbitCamera.VerticalAxis = vertical;

            inputData.isChange = false;
        }

        private void UpdateCameraDistance()
        {
            Character main = GameCharacterManager.Instance.GetMainCharacter();
            if(main)
            {
                if (main.IsBusy) return;
            }

            float scrollValue = Input.GetAxis("Mouse ScrollWheel");

            inputData.ChangeZoom(inputData.zoomDistance - scrollValue * Time.deltaTime * inputData.zoomSpeed);
            float velocity = 0;
            orbitCamera.Radius = Mathf.SmoothDamp(orbitCamera.Radius, inputData.zoomDistance, ref velocity, inputData.zoomSmooth);
        }
    }
}


