using PlayerController;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Invokes sound playback when the player enters the trigger.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionTutorialSound : TutorialSound
    {
        private void OnTriggerEnter(Collider other)
        {
            // request sound playback when player enters trigger and sound has not been played yet
            if (!other.CompareTag(PlayerProfileService.k_PlayerGameObjectTag)) return;
            
            RequestSoundPlayBack();
        }
    }
}