using UnityEngine;

namespace LA.Common.UI.Runtime
{
    [CreateAssetMenu(fileName = "Dropdown Item Hodler", menuName = "LA/Common/UI/Dropdown/Dropdown Item Hodler")]
    public class DropdownItemDataHolder : ScriptableObject
    {
        [SerializeField] private DropdownItemData[] datas;

        public DropdownItemData[] Datas => datas;

        public DropdownItemData GetByArrId(int id)
        {
            if (id >= 0 && id < datas.Length)
            {
                return datas[id];
            }

            return null;
        }
    }
}


