using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Others
{
 
    //Handling draw line for power wire in game
    public class PowerPolesManager : MonoBehaviour
    {
        [SerializeField] private LineRenderer[] lineRenderers;
        [SerializeField] private int resolution;
        [SerializeField] private float weight;
        [SerializeField] private AnimationCurve smoothCurve;
         
        private PowerPolesElement[] powerPolesElement;

        private async void Start()
        {
            powerPolesElement = GetComponentsInChildren<PowerPolesElement>();
            if (powerPolesElement != null)
            {
                await DrawPowerWire(powerPolesElement);
            }
        }

        private Task DrawPowerWire(PowerPolesElement[] powerPolesElements)
        {
            for(int i=0;i< lineRenderers.Length; i++)
            {
                Vector3[] linkPoints = new Vector3[powerPolesElements.Length+1];
                linkPoints[linkPoints.Length-1] = powerPolesElement[0].GetLinkPoint(i).position;
                for (int j=0;j<powerPolesElements.Length;j++)
                {
                    linkPoints[j] = powerPolesElement[j].GetLinkPoint(i).position;
                }

                List<Vector3> pointsSmooth = new List<Vector3>();

                for(int k = 0;k<linkPoints.Length-1; k++)
                {
                    if(pointsSmooth.Count>0)
                    {
                        pointsSmooth.Remove(pointsSmooth[pointsSmooth.Count-1]);
                    }

                    pointsSmooth.AddRange(SmoothDrawing(linkPoints[k], linkPoints[k + 1]));
                }

                lineRenderers[i].positionCount = pointsSmooth.Count;
                lineRenderers[i].SetPositions(pointsSmooth.ToArray());
            }

            return Task.CompletedTask;
        }

        public Vector3[] SmoothDrawing(Vector3 point1, Vector3 point2)
        {
            if(resolution <=1)
            {
                return new Vector3[] { point1, point2 };
            }

            Vector3[] smoothPoints = new Vector3[2 + resolution - 1];
            float resolutionUnit = (float)1 / resolution;

            for(int i=0; i< smoothPoints.Length; i++)
            {
                float T = resolutionUnit * i;
                Vector3 calculatedPoint = Vector3.Lerp(point1, point2, T);
                calculatedPoint.y -= smoothCurve.Evaluate(T) * weight;
                smoothPoints[i] = calculatedPoint;
            }

            return smoothPoints;
        }
    }

}

