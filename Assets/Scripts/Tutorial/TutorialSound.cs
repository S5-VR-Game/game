using System;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Abstract superclass, which provides functionality to request sound playback and to play the sound.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class TutorialSound : MonoBehaviour
    {
        private AudioSource m_AudioSource;
        private TutorialSoundService m_TutorialSoundService;
        private bool m_AlreadyPlayed;
        
        protected virtual void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_TutorialSoundService = FindObjectOfType<TutorialSoundService>();
            // check if tutorial sound service is available
            if (m_TutorialSoundService == null)
            {
                throw new Exception("TutorialSoundService not found in scene");
            }
        }
        
        /// <summary>
        /// Requests sound playback to the tutorial sound service. Ensures, that the sound is only played once.
        /// </summary>
        protected void RequestSoundPlayBack()
        {
            if (m_AlreadyPlayed)
            {
                return;
            }
            m_AlreadyPlayed = true;
            m_TutorialSoundService.RequestSoundPlayBack(this);
        }
        
        /// <summary>
        /// Plays the sound assigned to the audio source of this tutorial sound
        /// </summary>
        public void PlaySound()
        {
            m_AudioSource.Play();
        }

        /// <summary>
        /// Returns whether the audio source is currently playing.
        /// </summary>
        /// <returns>true, if the audio source currently plays a sound, otherwise false</returns>
        public bool IsPlaying()
        {
            return m_AudioSource.isPlaying;
        }

        /// <summary>
        /// Stops the playback of the audio source.
        /// </summary>
        public void StopPlaying()
        {
            m_AudioSource.Stop();
        }
    }
}