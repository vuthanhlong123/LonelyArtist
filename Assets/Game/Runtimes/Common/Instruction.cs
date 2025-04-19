using System.Threading.Tasks;
using UnityEngine;

namespace Game.Runtimes.Commons
{
    public class Instruction : MonoBehaviour
    {
        public virtual Task Run()
        {
            return Task.CompletedTask;
        }
    }
}


