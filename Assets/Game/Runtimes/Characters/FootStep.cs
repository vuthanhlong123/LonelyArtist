using UnityEngine;

namespace Game.Runtimes.Characters
{
    [RequireComponent(typeof(AudioSource))]
    public class FootStep : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        private AudioSource _AudioSource;

        private void Start()
        {
            _AudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 9)
            {
                if (_AudioSource)
                    _AudioSource.PlayOneShot(clips[0]);
            }
            else if(other.gameObject.layer == 8)
            {
                if (_AudioSource)
                    _AudioSource.PlayOneShot(clips[1]);
            }
        }
    }
}


