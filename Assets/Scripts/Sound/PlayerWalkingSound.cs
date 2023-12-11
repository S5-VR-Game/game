using UnityEngine;

namespace Sound
{
    public class PlayerWalkingSound : MonoBehaviour
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
                Debug.LogError("No sound found!");
            }
        }

        private void Update()
        {
            var movement = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");
            
            if (movement != 0f)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
            }
        }
    }
}