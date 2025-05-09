using System;
using UnityEngine;

namespace LA.Painting.Data
{
    [CreateAssetMenu(fileName = "Shape Paint Material Data", menuName = "LA/Painting/Shape Paint Material Data")]

    public class ShapePaintMaterialHolder : ScriptableObject
    {
        [SerializeField] private ShapePaintMaterialData[] shapePaintMaterials;

        public ShapePaintMaterialData[] ShapePaintMaterials => shapePaintMaterials;
        public ShapePaintMaterialData FindData(int id)
        {
            foreach (var item in shapePaintMaterials)
            {
                if(item.id == id) return item;
            }

            return null;
        }
    }

    [Serializable]
    public class ShapePaintMaterialData
    {
        public int id;
        public Material material;
        public bool useOpacity;
        public bool useWireWidth;
        public bool useSide;
        public bool useRadius;
        public bool useLineWidth;
        public bool useRotation;
    }
}

