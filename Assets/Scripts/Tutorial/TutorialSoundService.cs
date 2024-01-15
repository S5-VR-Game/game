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
        
        // if true, requests are accepted and queued, otherwise any requests will be ignored
        private bool m_AcceptRequests = true;

        /// <summary>
        /// Adds the given tutorial sound to the queue of requested sounds, if requests are accepted currently (see
        /// <see cref="m_AcceptRequests"/>). Otherwise the request will be ignored.
        /// </summary>
        /// <param name="tutorialSound">
        /// sound, which will be enqueued and played, when all previously requested sounds have finished playing
        /// </param>
        public void RequestSoundPlayBack(TutorialSound tutorialSound)
        {
            if (m_AcceptRequests)
            { 
                m_RequestedSounds.Enqueue(tutorialSound);
            }
        }

        private void Update()
        {
            // only play next sound if the last sound has finished playing and the queue is not empty
            if (m_LastPlayedSound != null && m_LastPlayedSound.IsPlaying() || m_RequestedSounds.Count <= 0) return;
            
            m_LastPlayedSound = m_RequestedSounds.Dequeue();
            m_LastPlayedSound.PlaySound();
        }

        /// <summary>
        /// Stops and cancels the current playback and proceeds with the next sound in the queue, if there is any.
        /// </summary>
        public void StopCurrentPlayBack()
        {
            m_LastPlayedSound.StopPlaying();
        }
        
        /// <summary>
        /// Allows/disallows requests for sound playback.
        /// </summary>
        /// <param name="acceptRequests">if true, requests are accepted and queued, otherwise any requests will be ignored</param>
        public void SetAcceptRequests(bool acceptRequests)
        {
            m_AcceptRequests = acceptRequests;
        }
    }
}