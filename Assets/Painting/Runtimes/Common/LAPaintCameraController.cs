using UnityEngine;

namespace LA.Common
{
    public class LAPaintCameraController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 horizontalBound;
        [SerializeField] private Vector2 verticalBound;

        private Vector3 oldMousePos;
        private float angelX;
        private float angelY;

        private Vector3 defaultAngle;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                oldMousePos = Input.mousePosition;
            }

            if(Input.GetMouseButton(1))
            {
                Vector3 error = Input.mousePosition - oldMousePos;
                oldMousePos = Input.mousePosition;

                angelX += -error.y * speed * Time.deltaTime;
                angelY += error.x * speed * Time.deltaTime;

                if (angelX < verticalBound.x) angelX = verticalBound.x;
                else if(angelX > verticalBound.y) angelX = verticalBound.y;

                if (angelY < horizontalBound.x) angelY = horizontalBound.x;
                else if (angelY > horizontalBound.y) angelY = horizontalBound.y;

                Vector3 targetAngle = defaultAngle;
                targetAngle.x += angelX;
                targetAngle.y += angelY;

                transform.eulerAngles = targetAngle;
            }
        }

        public void ResetState()
        {
            transform.localRotation = Quaternion.identity;
            defaultAngle = transform.eulerAngles;
        }

        private void OnEnable()
        {
            ResetState();
        }
    }
}

