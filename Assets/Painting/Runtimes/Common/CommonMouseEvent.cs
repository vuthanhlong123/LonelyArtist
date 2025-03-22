using UnityEngine;
using UnityEngine.EventSystems;

namespace LA.Painting.Common
{
    public class CommonMouseEvent : MonoBehaviour
    {
        public static bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}


