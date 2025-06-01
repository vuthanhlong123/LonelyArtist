using UnityEngine;

namespace Game.Runtimes.Cameras
{
    [CreateAssetMenu(fileName = "Camera Input Data", menuName = "Game/Cameras/Camera Input Data")]

    public class CameraInputData : ScriptableObject
    {
        public float speedX;
        public float speedY;

        public Vector2 axisValue;
        public float orbitX => axisValue.x * speedX;
        public float orbitY => axisValue.y * speedY;

        public float zoomDistance;
        public float zoomSpeed;
        public float minZoom;
        public float maxZoom;
        public float zoomSmooth;

        public void ChangeZoom(float value)
        {
            if (!isZoomAvailable) return;

            if (value < minZoom) zoomDistance = minZoom;
            else if (value > maxZoom) zoomDistance = maxZoom;
            else zoomDistance = value;
        }

        public bool isChange;
        public bool isZoomAvailable;
    }
}


