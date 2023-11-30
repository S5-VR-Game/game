using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void OnEnable()
        {
            UpdateActivePlayer();
        }

        /// <summary>
        /// Updates the current active player according to the <see cref="isVrPlayerActive"/> state
        /// </summary>
        private void UpdateActivePlayer()
        {
            if (SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
            {
                PlayerPrefs.SetString("CurrentPlayer", isVrPlayerActive ? "VR" : "Keyboard");
                Debug.Log("CurrentPlayer is" + PlayerPrefs.GetString("CurrentPlayer"));
            }

            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                keyBoardPlayer.SetActive(false);
                vrPlayer.SetActive(true);
            }
            else
            {
                keyBoardPlayer.SetActive(true);
                vrPlayer.SetActive(false);
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
    }
}