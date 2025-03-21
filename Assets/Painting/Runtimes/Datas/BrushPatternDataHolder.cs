using System;
using UnityEngine;

namespace LA.Painting.Data
{
    [CreateAssetMenu(fileName = "Brush Pattern Data", menuName = "LA/Painting/Brush Pattern Data")]
    public class BrushPatternDataHolder : ScriptableObject
    {
        [SerializeField] private BrushPatternData[] brushPatterns;

        public BrushPatternData[] BrushPatterns => brushPatterns;

        public BrushPatternData GetByArrId(int arrId)
        {
            if(arrId <0 || arrId >= brushPatterns.Length) return null;

            return brushPatterns[arrId];
        }
    }

    [Serializable]
    public class BrushPatternData
    {
        [SerializeField] private string brushName;
        [SerializeField] private Texture2D brushTexture;
        [SerializeField] private Sprite brushSprite;

        public string BrushName => brushName;
        public Texture2D BrushTexture => brushTexture;
        public Sprite BrushSprite => brushSprite;
    }
}


