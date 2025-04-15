using UnityEngine;

namespace Game.Runtimes.UIs
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private string id;

        public string ID => id;

        private void Awake()
        {
            if(GameUIManager.Instance == null)
            {
                GameObject newobj = new GameObject("Game UI Manager");
                GameUIManager uiManager = newobj.AddComponent<GameUIManager>();
                uiManager.Add(this);
            }
            else
            {
                GameUIManager.Instance.Add(this);
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


