using UnityEngine;

namespace Game.Runtimes.Objects
{
    public class ObjectElement : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private bool disableOnStart;

        public string ID => id;

        private void Awake()
        {
            if (GameObjectManager.Instance == null)
            {
                GameObject newobj = new GameObject("Game Object Manager");
                GameObjectManager manager = newobj.AddComponent<GameObjectManager>();
                manager.Add(this);
            }
            else
            {
                GameObjectManager.Instance.Add(this);
            }
        }

        private void Start()
        {
            if (id == "") id = gameObject.name;

            if(disableOnStart) gameObject.SetActive(false);
        }

        public void Show(bool status)
        {
            gameObject.SetActive(status);
        }
    }
}

