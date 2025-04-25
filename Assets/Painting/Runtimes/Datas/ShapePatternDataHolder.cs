using System;
using UnityEngine;

namespace LA.Painting.Data
{
    [CreateAssetMenu(fileName = "Shape Pattern Data", menuName = "LA/Painting/Shape Pattern Data")]

    public class ShapePatternDataHolder : ScriptableObject
    {
        [SerializeField] private ShapePatternData[] shapePatterns;

        public ShapePatternData[] ShapePatterns => shapePatterns;

        public ShapePatternData GetByArrId(int arrId)
        {
            if (arrId < 0 || arrId >= shapePatterns.Length) return null;

            return shapePatterns[arrId];
        }

        public ShapePatternData GetById(int Id)
        {
            foreach (ShapePatternData patternData in shapePatterns)
            {
                if(patternData.ID == Id) return patternData;
            }

            return null;
        }
    }

    [Serializable]
    public class ShapePatternData
    {
        [SerializeField] private int id;
        [SerializeField] private int materialReferenceId;
        [SerializeField] private Texture2D texture;
        [SerializeField] private Sprite sprite;

        public int ID => id;
        public int MaterialReferenceId => materialReferenceId;
        public Texture2D Texture => texture;
        public Sprite Sprite => sprite;
    }
}

