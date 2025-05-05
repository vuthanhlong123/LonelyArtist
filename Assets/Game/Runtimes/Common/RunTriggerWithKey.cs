using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class RunTriggerWithKey : MonoBehaviour
    {
        [SerializeField] private KeyCode key;
        [SerializeField] private Trigger trigger;

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                trigger.Run();
            }
        }
    }
}


