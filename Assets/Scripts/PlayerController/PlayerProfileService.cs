using UnityEngine;

namespace PlayerController
{
    /// <summary>
    /// This Service determines whether VRPlayer or KeyboardPlayer
    /// should be activated or not
    /// </summary>
    public class PlayerProfileService : MonoBehaviour
    {
        [SerializeField] public bool isVrPlayerActive;
        [SerializeField] public GameObject vrPlayer;
        [SerializeField] public GameObject keyBoardPlayer;
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void Start()
        {
            if (isVrPlayerActive)
            {
                keyBoardPlayer.SetActive(false);
            }
            else
            {
                vrPlayer.SetActive(false);
            }
        }
    }
}