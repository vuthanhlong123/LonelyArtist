using System;
using UnityEngine;

namespace Game.Runtimes.Others
{
    public class ShipWaveController : MonoBehaviour
    {
        public enum Type
        {
            Up,Down
        }
        [SerializeField] private float duration;
        [SerializeField] private float height;
        [SerializeField] private float depth;
        [SerializeField] private float angleUp;
        [SerializeField] private float angleDown;

        [SerializeField] private AnimationCurve curve;

        private Type type;
        private Vector3 defaultPos;
        private Vector3 defaultAngle;

        private Vector3 startPos;
        private Vector3 startAngle;

        private Vector3 endPos;
        private Vector3 endAngle;

        private float currentDuration;

        private void Start()
        {
            defaultPos = transform.position;
            startPos = transform.position;
            endPos = defaultPos - new Vector3(0, depth, 0);

            defaultAngle = transform.eulerAngles;
            startAngle = defaultAngle;
            endAngle = defaultAngle + new Vector3(0,0, angleUp);
        }

        private void FixedUpdate()
        {
            switch(type)
            {
                case Type.Up:
                    HandlingUp(); break;
                case Type.Down:
                    HandlingDown(); break;
            }
        }

        private void HandlingUp()
        {
            currentDuration += Time.deltaTime;
            float linear = curve.Evaluate(currentDuration/duration);
            transform.position = Vector3.Lerp(startPos, endPos, linear);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(startAngle.x, endAngle.x, linear), transform.eulerAngles.y, Mathf.LerpAngle(startAngle.z, endAngle.z, linear));

            if(currentDuration >= duration)
            {
                currentDuration = 0;
                type = Type.Down;
                startPos = transform.position;
                endPos = defaultPos + new Vector3(0, UnityEngine.Random.Range(0, depth), 0);

                startAngle = transform.eulerAngles;
                endAngle = defaultAngle + new Vector3(UnityEngine.Random.Range(0, angleDown), 0, UnityEngine.Random.Range(0,angleDown));
            }
        }

        private void HandlingDown()
        {
            currentDuration += Time.deltaTime;
            float linear = curve.Evaluate(currentDuration / duration);
            transform.position = Vector3.Lerp(startPos, endPos, linear);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(startAngle.x, endAngle.x, linear), transform.eulerAngles.y, Mathf.LerpAngle(startAngle.z, endAngle.z, linear));

            if (currentDuration >= duration)
            {
                currentDuration = 0;
                type = Type.Up;
                startPos = transform.position;
                endPos = defaultPos + new Vector3(0, UnityEngine.Random.Range(0, height), 0);

                startAngle = transform.eulerAngles;
                endAngle = defaultAngle + new Vector3(UnityEngine.Random.Range(0, angleUp), 0, UnityEngine.Random.Range(0, angleUp));
            }
        }
    }
}


