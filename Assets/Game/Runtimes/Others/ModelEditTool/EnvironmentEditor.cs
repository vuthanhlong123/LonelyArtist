using UnityEngine;

namespace Game.Runtimes.Others
{
    public class EnvironmentEditor : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private GameObject generationObj;

        private void Start()
        {
            Camera mainCam = Camera.main;
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, targetLayer))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Camera mainCam = Camera.main;
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit hit, 200, targetLayer))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}


