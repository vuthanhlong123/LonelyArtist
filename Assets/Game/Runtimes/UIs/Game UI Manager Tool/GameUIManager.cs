using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtimes.UIs
{
    public class GameUIManager : MonoBehaviour
    {
        public static GameUIManager Instance;

        private List<UIElement> list = new List<UIElement>();

        private void Awake()
        {
            Instance = this;
        }

        public void Add(UIElement element)
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

        public void EnableUIs(string[] targets, bool status)
        {
            foreach (UIElement ui in list)
            {
                if (IsContain(ui.ID, targets))
                {
                    ui.Show(status);
                }
            }
        }

        public void EnableAllUIExcept(string[] excepts, bool status)
        {
            foreach (UIElement ui in list)
            {
                if(!IsContain(ui.ID, excepts))
                {
                    ui.Show(status);
                }
            }
        }

        private bool IsContain(string checkId, string[] TargetArr)
        {
            foreach(string id in TargetArr)
            {
                if(id == checkId) return true;
            }
            return false;
        }
    }
}


