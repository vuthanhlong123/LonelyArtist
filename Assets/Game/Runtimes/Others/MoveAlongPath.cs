using UnityEngine;
using UnityEngine.Splines;

namespace Game.Runtimes.Others
{
    [RequireComponent(typeof(LineRenderer))]
    public class MoveAlongPath : MonoBehaviour
    {
        [Header("Path")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float speed;

        private int currentDestinationIndex;
        private Vector3 currentDestination;
        private Vector3 targetPos;
        private Vector3 targetDirection;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                this.enabled = false;
                return;
            }
               
            currentDestinationIndex = 0;
            currentDestination = lineRenderer.GetPosition(currentDestinationIndex);
        }

        private void FixedUpdate()
        {
            targetPos = Vector3.MoveTowards(transform.position, currentDestination, speed*Time.fixedDeltaTime);
            targetDirection = targetPos - transform.position;
            //targetDirection.x = 0;
            Vector3 velocity = Vector3.zero;
            Vector3 eulerAngle = Vector3.SmoothDamp(transform.eulerAngles, Quaternion.LookRotation(targetDirection, Vector3.up).eulerAngles, ref velocity, 0.1f);
            transform.eulerAngles = eulerAngle;
            transform.position = targetPos;

            if(Vector3.Distance(transform.position, currentDestination) <=1)
            {
                currentDestinationIndex = currentDestinationIndex + 1 < lineRenderer.positionCount ? currentDestinationIndex+1 : 0;

                currentDestination = lineRenderer.GetPosition(currentDestinationIndex);
            }
        }
    }
}


