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
        [SerializeField] public GameObject xrOrigin;
        [SerializeField] public GameObject keyBoardPlayer;
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void Start()
        {
            if (isVrPlayerActive)
            {
                keyBoardPlayer.SetActive(false);
                PlayerPrefs.SetString("CurrentPlayer", "VR");
            }
            else
            {
                vrPlayer.SetActive(false);
                PlayerPrefs.SetString("CurrentPlayer", "Keyboard");
            }
        }

        /// <summary>
        /// Returns the current player game object, which is either the xrOrigin or the keyboardPlayer.
        /// This game object can be used to obtain the current player position, orientation, scale etc.
        /// </summary>
        public GameObject getPlayerGameObject()
        {
            return isVrPlayerActive ? xrOrigin : keyBoardPlayer;
        }
    }
}