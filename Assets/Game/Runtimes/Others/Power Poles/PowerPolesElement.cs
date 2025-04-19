using UnityEngine;

namespace Game.Runtimes.Others
{
    public class PowerPolesElement : MonoBehaviour
    {
        [SerializeField] private Transform[] linkPoints;

        public Transform GetLinkPoint(int id)
        {
            if(id<0 || id>= linkPoints.Length) return null;

            return linkPoints[id];
        }
    }
}

