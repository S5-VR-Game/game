using PlayerController;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Invokes the <see cref="TutorialProcedure.NextTutorialState"/> method to continue the tutorial when the player
    /// enters the trigger.
    /// Will fire only a single time to avoid duplicate calls.
    /// </summary>
    public class NextTutorialStateOnPlayerTrigger : MonoBehaviour
    {
        [SerializeField] private TutorialProcedure tutorialProcedure;
        private bool m_Fired;
        
        private void OnTriggerEnter(Collider other)
        {
            // check if player entered and if the event was not fired yet
            if (!other.CompareTag(PlayerProfileService.k_PlayerGameObjectTag) || m_Fired) return;
            m_Fired = true;
            tutorialProcedure.NextTutorialState();
        }
    }
}