using Game.Runtimes.Cameras;
using Game.Runtimes.UIs;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtimes.Objects
{
    public class GameObjectManager : MonoBehaviour
    {
        public static GameObjectManager Instance;

        [SerializeField] private List<ObjectElement> list = new List<ObjectElement>();

        private void Awake()
        {
            Instance = this;
        }

        public void Add(ObjectElement element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }

        public void Enable(string id)
        {
            Enable(id, true);
        }

        public void Disable(string id)
        {
            Enable(id, false);
        }

        public void Enable(string id, bool status)
        {
            foreach (var element in list)
            {
                if (element.ID == id)
                {
                    element.Show(status);
                    break;
                }
            }
        }

        public void EnableObjects(string[] targets, bool status)
        {
            foreach (var element in list)
            {
                if (IsContain(element.ID, targets))
                {
                    element.Show(status);
                }
            }
        }

        public void EnableAllExcept(string[] excepts, bool status)
        {
            foreach (var element in list)
            {
                if (!IsContain(element.ID, excepts))
                {
                    element.Show(status);
                }
            }
        }

        private bool IsContain(string checkId, string[] TargetArr)
        {
            foreach (string id in TargetArr)
            {
                if (id == checkId) return true;
            }
            return false;
        }
    }
}


