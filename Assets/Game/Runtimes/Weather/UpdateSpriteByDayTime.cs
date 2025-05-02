using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtimes.Weather
{
    public class UpdateSpriteByDayTime : MonoBehaviour
    {
        [Serializable]
        public class UpdateElement
        {
            public Vector2 timeRange;
            public Sprite sprite;
        }

        [SerializeField] private Image targetImage;
        [SerializeField] private UpdateElement[] updateElements;

        private UpdateElement currentElement = null;

        private void Update()
        {
            float dayTime = DayTimeController.staticTimeOfDay;

            foreach (UpdateElement updateElement in updateElements)
            {
                if (dayTime >= updateElement.timeRange.x && dayTime < updateElement.timeRange.y)
                {
                    if (currentElement != null)
                    {
                        if (currentElement == updateElement) return;
                    }

                    targetImage.sprite = updateElement.sprite;
                    currentElement = updateElement;

                    break;
                }
            }
        }
    }

}

