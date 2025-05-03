using UnityEngine;

namespace LA.Painting.Common
{
    public class LAPaintControl_Undo : MonoBehaviour
    {
        [SerializeField] private LAPaintingDataManager dataManager;
        public void Execute()
        {
            dataManager.HandlingUndo();
        }
    }
}


