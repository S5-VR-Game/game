using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Manages the playback order of the requested tutorial sounds (FIFO) and ensures
    /// that only one sound is played at a time. 
    /// </summary>
    public class TutorialSoundService : MonoBehaviour
    {
        private readonly Queue<TutorialSound> m_RequestedSounds = new();
        
        private TutorialSound m_LastPlayedSound;

        /// <summary>
        /// Adds the given tutorial sound to the queue of requested sounds.
        /// </summary>
        /// <param name="tutorialSound">
        /// sound, which will be enqueued and played, when all previously requested sounds have finished playing
        /// </param>
        public void RequestSoundPlayBack(TutorialSound tutorialSound)
        {
            print("Sound requested");
            m_RequestedSounds.Enqueue(tutorialSound);
        }

        private void Update()
        {
            // debug input to skip current sound
            if (Input.GetKeyDown(KeyCode.M))
            {
                m_LastPlayedSound.StopPlaying();
            }
            
            // only play next sound if the last sound has finished playing and the queue is not empty
            if (m_LastPlayedSound != null && m_LastPlayedSound.IsPlaying() || m_RequestedSounds.Count <= 0) return;
            
            m_LastPlayedSound = m_RequestedSounds.Dequeue();
            m_LastPlayedSound.PlaySound();
        }
    }
}