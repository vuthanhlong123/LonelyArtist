using UnityEngine;

namespace Game.Runtimes.Others
{
    public class RotationController : MonoBehaviour
    {
        public enum Type
        {
            rotateX, rotateY, rotateZ
        }

        [SerializeField] private float speed;
        [SerializeField] private Type type;
        [SerializeField] private float startAngle;
        [SerializeField] private bool randomStartAngle;

        private float angle;

        private void Start()
        {
            if(randomStartAngle)
            {
                angle = Random.Range(0, startAngle);
            }
            else
            {
                angle = startAngle;
            }
        }

        private void FixedUpdate()
        {
            switch (type)
            {
                case Type.rotateX: RotatingXAsix(); break;
                case Type.rotateY: RotatingYAsix(); break;
                case Type.rotateZ: RotatingZAsix(); break;
            }
        }

        private void RotatingXAsix()
        {
            angle += Time.fixedDeltaTime * speed;
            transform.localEulerAngles = new Vector3(angle, 0, 0);

            if(angle >= 360)
            {
                angle = 0;
            }
        }

        private void RotatingYAsix()
        {
            angle += Time.fixedDeltaTime * speed;
            transform.localEulerAngles = new Vector3(0, angle, 0);

            if (angle >= 360)
            {
                angle = 0;
            }
        }

        private void RotatingZAsix()
        {
            angle += Time.fixedDeltaTime * speed;
            transform.localEulerAngles = new Vector3(0, 0, angle);

            if (angle >= 360)
            {
                angle = 0;
            }
        }
    }
}


