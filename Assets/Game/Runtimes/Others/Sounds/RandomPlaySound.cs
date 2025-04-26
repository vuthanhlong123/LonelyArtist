using UnityEngine;

namespace Game.Runtimes.Others
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomPlaySound : MonoBehaviour
    {
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;

        private AudioSource audioSource;
        private float delayDuration;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            delayDuration -= Time.fixedDeltaTime;
            if (delayDuration <= 0)
            {
                PlaySound();

                delayDuration = Random.Range(minTime, maxTime);
            }
        }

        private void PlaySound()
        {
            if (audioSource.isPlaying) return;

            audioSource.Play();
        }
    }
}

