using System;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// class used to play sounds depending on the chosen value of playSoundTrigger
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        /// <summary>
        /// enum to decide when the sound should be played
        /// </summary>
        private enum PlaySoundTrigger
        {
            FunctionCall, // plays sound after calling PlaySound() in another script
            Collision, // plays sound after the game-object collides with something
            PlayerMovement, // plays sound when the player is moving
            Permanent, // plays sound permanently
            Rolling // plays sound while collision stays and game-object is moving
        }
        
        [SerializeField] private AudioClip audioClip;  // stores the played audio file
        
        [Tooltip("sets the volume (from 0 (silent) to 1 (loud)")]
        [SerializeField] private float volumeSound; // sets the volume of a sound

        [SerializeField] private PlaySoundTrigger playSoundTrigger; // value how the sound should be played

        [Tooltip("False: makes the sound audible in a distance of 15; True: makes the sound audible in the whole scene")]
        [SerializeField] private bool playSoundGlobal; // variable to make the sound audible local (range of 15) or global
        
        private AudioSource _audioSource; // stores the audio-source (needed to play the sound)
        
        private void Start()
        {
            // adds the audio-source to all game-objects who has this script
            _audioSource = gameObject.AddComponent<AudioSource>();

            if (audioClip == null) return;
            
            _audioSource.clip = audioClip;
            _audioSource.volume = volumeSound;
            _audioSource.playOnAwake = false;
            
            if (playSoundGlobal) // if the sound should be audible global
            {
                // makes the sound audible in a distance of 0 and 5000 units
                _audioSource.maxDistance = 5000f;
                _audioSource.minDistance = 0f;
            }
            else // if the sound should be audible local
            {
                // makes the sound audible in a distance of 0 and 15 units
                _audioSource.maxDistance = 15f;
                _audioSource.minDistance = 0f;
                
                _audioSource.spatialBlend = 1f; // makes the sound 3D to make the sound louder/quieter depending on the
                                                // distance between the game-object and the audio listener from the player
                _audioSource.rolloffMode = AudioRolloffMode.Linear; // uses linear control of distance/volume
            }
        }

        private void Update()
        {
            // PlaySoundTrigger = PlayerMovement
            // plays sound if the player moves
            if (playSoundTrigger.Equals(PlaySoundTrigger.PlayerMovement))
            {
                var movement = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");

                if (movement != 0f) // if the player is walking
                {
                    StartPlayingSound(); // plays sound
                }
                else // if the player is standing still
                {
                    StopPlayingSound(); // stops sound
                }
            }

            // PlaySoundTrigger = Permanent
            // plays sound permanently
            if (playSoundTrigger.Equals(PlaySoundTrigger.Permanent))
            {
                StartPlayingSound(); // plays sound
            }
        }

        /// <summary>
        /// PlaySoundTrigger = Collision
        /// plays sound on collision
        /// </summary>
        private void OnCollisionEnter()
        {
            if (playSoundTrigger.Equals(PlaySoundTrigger.Collision))
            {
                StartPlayingSound(); // plays sound
            }
        }

        /// <summary>
        /// PlaySoundTrigger = Rolling
        /// plays sound while collision stays and game-object is moving
        /// </summary>
        private void OnCollisionStay()
        {
            if (!playSoundTrigger.Equals(PlaySoundTrigger.Rolling)) return;
            
            var velocity = gameObject.GetComponent<Rigidbody>().velocity;
            
            if (velocity.x + velocity.y + velocity.z != 0)
            {
                StartPlayingSound();
            }
            else
            {
                StopPlayingSound();
            }
        }

        /// <summary>
        /// PlaySoundTrigger = FunctionCall
        /// plays sound with function call
        /// </summary>
        public void PlaySoundFunctionCall()
        {
            if (!playSoundTrigger.Equals(PlaySoundTrigger.FunctionCall)) return;
            
            StartPlayingSound(); // plays sound
        }
        
        /// <summary>
        /// starts playing sound
        /// </summary>
        private void StartPlayingSound()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        /// <summary>
        /// stops playing sound
        /// </summary>
        private void StopPlayingSound()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}