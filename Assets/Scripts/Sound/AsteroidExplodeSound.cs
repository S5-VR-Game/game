using UnityEngine;

namespace Sound
{
    public class AsteroidExplodeSound : MonoBehaviour
    {
        public AudioClip audioClip;  // stores the played audio file
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (audioClip != null)
            {
                _audioSource.clip = audioClip;
            }
            else
            {
                Debug.LogError("No Sound found!");
            }
        }

        public void PlayAsteroidExplodeSoundOnce()
        {
            _audioSource.Play();
        }
    }
}