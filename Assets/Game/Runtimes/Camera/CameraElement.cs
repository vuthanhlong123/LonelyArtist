using UnityEngine;

namespace Game.Runtimes.Cameras
{
    public class CameraElement : MonoBehaviour
    {
        [SerializeField] private string id;

        public string ID => id;

        private void Awake()
        {
            if (GameCameraManager.Instance == null)
            {
                GameObject newobj = new GameObject("Game Camera Manager");
                GameCameraManager manager = newobj.AddComponent<GameCameraManager>();
                manager.Add(this);
            }
            else
            {
                GameCameraManager.Instance.Add(this);
            }
        }

        private void Start()
        {
            if (id == "") id = gameObject.name;
        }

        public void Show(bool status)
        {
            gameObject.SetActive(status);
        }
    }
}


