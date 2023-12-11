using UnityEngine;

namespace PlayerController
{
    /// <summary>
    /// This Service determines whether VRPlayer or KeyboardPlayer
    /// should be activated or not
    /// </summary>
    public class PlayerProfileService : MonoBehaviour
    {
        [SerializeField] private bool isVrPlayerActive;
        [SerializeField] private GameObject vrPlayer;
        [SerializeField] private GameObject xrOrigin;
        [SerializeField] private GameObject keyBoardPlayer;
        [SerializeField] private Transform leftVrController;
        [SerializeField] private Transform rightVrController;
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void Start()
        {
            UpdateActivePlayer();
        }

        /// <summary>
        /// Updates the current active player according to the <see cref="isVrPlayerActive"/> state
        /// </summary>
        private void UpdateActivePlayer()
        {
            keyBoardPlayer.SetActive(!isVrPlayerActive);
            vrPlayer.SetActive(isVrPlayerActive);
            if (isVrPlayerActive)
            {
                PlayerPrefs.SetString("CurrentPlayer", "VR");
            }
            else
            {
                PlayerPrefs.SetString("CurrentPlayer", "Keyboard");
            }
        }

        /// <summary>
        /// Returns the current player game object, which is either the xrOrigin or the keyboardPlayer.
        /// This game object can be used to obtain the current player position, orientation, scale etc.
        /// </summary>
        public GameObject GetPlayerGameObject()
        {
            return isVrPlayerActive ? xrOrigin : keyBoardPlayer;
        }

        public bool GetIsVrPlayerActive()
        {
            return isVrPlayerActive;
        }

        /// <summary>
        /// Changes the current active player according to the given value
        /// </summary>
        /// <param name="vrPlayerActive">
        /// if true, the vr player will be activated otherwise the keyboard player will be activated
        /// </param>
        public void SetIsVrPlayerActive(bool vrPlayerActive)
        {
            isVrPlayerActive = vrPlayerActive;
            UpdateActivePlayer();
        }

        /// <summary>
        /// Getter for left vr controller
        /// </summary>
        /// <returns>transform of the left vr controller</returns>
        public Transform GetLeftVrController()
        {
            return leftVrController;
        }

        /// <summary>
        /// Getter for right vr controller
        /// </summary>
        /// <returns>transform of the right vr controller</returns>
        public Transform GetRightVrController()
        {
            return rightVrController;
        }
    }
}