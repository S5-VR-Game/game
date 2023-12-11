using PlayerController;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Connects the hud of the player with the game data.
    /// Updates the hud elements after game events like OnIntegrityChanged and OnTimeChanged.
    /// </summary>
    public class GameHUDService : MonoBehaviour
    {
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private Integrity integrity;
        [SerializeField] private GameTimer gameTimer;

        private void Start()
        {
            gameTimer.OnTimeChanged += OnTimeChanged;
            // deactivated due to workaround (see below)
            // integrity.OnIntegrityChanged += OnIntegrityChanged;
        }

        private void Update()
        {
            // workaround to update integrity indicator on HUD
            // if UpdateIntegrityIndicator is invoked multiple times with the same value, the bar will update
            // its value, if invoked once only, the bar will not be updated correctly
            // this seems to be a bug in the changeBar method of the integrity indicator
            // (maybe a error with the Mathf.Lerp call and the over-time calculation, if only invoked once compared to
            // invoked multiple times with the same parameter?!)
            // TODO if the bug is fixed, register the OnIntegrityChanged method in the start method
            // or remove the unused listener method if the bug can not be fixed
            playerProfileService.GetHUD().UpdateIntegrityIndicator(integrity.GetCurrentIntegrityPercentage());
        }

        /// <summary>
        /// Event listener for time changes. Updates the time indicator on the HUD.
        /// </summary>
        /// <param name="remainingTime">current remaining time of the game timer</param>
        private void OnTimeChanged(float remainingTime)
        {
            playerProfileService.GetHUD().UpdateTime(remainingTime);
        }

        /// <summary>
        /// Event listener for integrity changes. Updates the integrity indicator on the HUD.
        /// </summary>
        /// <param name="integrityValue">new integrity value</param>
        private void OnIntegrityChanged(int integrityValue)
        {
            playerProfileService.GetHUD().UpdateIntegrityIndicator(integrity.GetCurrentIntegrityPercentage());
        }
    }
}
