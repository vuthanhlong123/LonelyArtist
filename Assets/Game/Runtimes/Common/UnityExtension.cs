using UnityEngine;

namespace Game.Runtimes.Commons
{
    public static class UnityExtension 
    {
        public static bool ContainLayer(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}

