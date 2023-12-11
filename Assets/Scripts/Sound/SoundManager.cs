using PlayerController;
using UnityEngine;

namespace Sound
{
    // class used to play sounds depending on the chosen value of playSoundTrigger
    public class SoundManager : MonoBehaviour
    {
        public AudioClip audioClip;  // stores the played audio file
        private AudioSource _audioSource; // stores the audio-source (needed to play the sound)

        // enum to decide when the sound should be played
        public enum PlaySoundTrigger
        {
            FunctionCall, // plays sound after calling PlaySound() in another script
            Collision, // plays sound after the game-object collides with something
            PlayerMovement, // plays sound when the player is moving
            PlayerDistance, // plays sound when the player is a given range to the game-object
            Permanent // plays sound permanently
        }

        public PlaySoundTrigger playSoundTrigger; // value how the sound should be played
        
        private bool _isPlaying; // value to stop the sound for the enum PlayerMovement
        
        
        [Tooltip("You only need to set a value here if you are using the Play Sound Trigger 'Player Distance'.")]
        public PlayerProfileService playerProfileService; // stores the reference to the player
        
        [Tooltip("You only need to set a value here if you are using the Play Sound Trigger 'Player Distance'.")]
        public float playerDistance; // threshold value for the distance between the game-object and the player
                                     // real distance > threshold -> no sound
                                     // real distance <= threshold -> sound
        private void Start()
        {
            // adds the audio-source to all game-objects who has this script
            _audioSource = gameObject.AddComponent<AudioSource>();
            if (audioClip != null)
            {
                _audioSource.clip = audioClip;
                _audioSource.playOnAwake = false;
            }
            else
            {
                Debug.LogError("No Sound found!");
            }
        }

        private void Update()
        {
            // PlaySoundTrigger = PlayerMovement
            // plays sound if the player moves
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

            // PlaySoundTrigger = PlayerDistance
            // plays sound if the player has a specific distance to the game-object
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

            // PlaySoundTrigger = Permanent
            // plays sound permanently
            if (playSoundTrigger.Equals(PlaySoundTrigger.Permanent))
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                    _audioSource.volume = 0.5f;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            // PlaySoundTrigger = Permanent
            // plays sound on collision
            if (playSoundTrigger.Equals(PlaySoundTrigger.Collision))
            {
                _audioSource.Play();
            }
        }
        
        public void PlaySound()
        {
            // PlaySoundTrigger = Permanent
            // plays sound with function call
            if (!playSoundTrigger.Equals(PlaySoundTrigger.FunctionCall)) return;
            _audioSource.Play();
        }
    }
}