using PlayerController;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public AudioClip audioClip;  // stores the played audio file
        private AudioSource _audioSource;

        public enum PlaySoundTrigger
        {
            FunctionCall,
            Collision,
            PlayerMovement,
            PlayerDistance
        }

        public PlaySoundTrigger playSoundTrigger;
        private bool _isPlaying;
        
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            if (audioClip != null)
            {
                _audioSource.clip = audioClip;
            }
            else
            {
                Debug.LogError("No Sound found!");
            }
        }
        
        public PlayerProfileService playerProfileService;
        public float playerDistance;

        private void Update()
        {
            if (playSoundTrigger.Equals(PlaySoundTrigger.PlayerMovement))
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

            if (playSoundTrigger.Equals(PlaySoundTrigger.PlayerDistance))
            {
                if (Vector3.Distance(gameObject.transform.position,
                        playerProfileService.GetPlayerGameObject().transform.position) <= playerDistance && !_isPlaying)
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

        private void OnCollisionEnter(Collision other)
        {
            if (playSoundTrigger.Equals(PlaySoundTrigger.Collision))
            {
                _audioSource.Play();
            }
        }
        
        public void PlaySound()
        {
            if (!playSoundTrigger.Equals(PlaySoundTrigger.FunctionCall)) return;
            _audioSource.Play();
        }

        private System.Collections.IEnumerator PlayAudio()
        {
            _isPlaying = true;
            
            _audioSource.PlayOneShot(audioClip);
            
            yield return new WaitForSeconds(audioClip.length);

            _isPlaying = false;
        }
    }
}