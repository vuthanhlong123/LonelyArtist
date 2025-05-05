using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Runtimes.Menu
{
    public class PointerEventPlaySound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private AudioSource m_AudioSource;
        [SerializeField] private AudioClip clip_Hover;
        [SerializeField] private AudioClip clip_Click;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(m_AudioSource != null)
                m_AudioSource.PlayOneShot(clip_Click);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_AudioSource != null)
                m_AudioSource.PlayOneShot(clip_Hover);
        }
    }
}


