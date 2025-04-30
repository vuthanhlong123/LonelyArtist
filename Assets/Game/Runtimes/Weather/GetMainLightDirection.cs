using UnityEngine;

namespace Game.Runtimes.Weather
{
    [ExecuteInEditMode]
    public class GetMainLightDirection : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private void Update()
        {
            _material.SetVector("_MainlightDirection", transform.forward);
        }
    }
}

