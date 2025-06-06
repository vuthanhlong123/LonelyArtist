using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Runtimes.Commons
{
    public static class UnityExtension 
    {
        public static string SavePaintFolderPath = Application.persistentDataPath + "/PaintingSaved";

        public static bool ContainLayer(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}

