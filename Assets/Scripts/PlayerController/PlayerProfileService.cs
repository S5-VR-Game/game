using System;
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
        [SerializeField] private Transform leftVrController;
        [SerializeField] private Transform rightVrController;
        [SerializeField] private Camera vrCamera;
        [SerializeField] private Camera keyboardCamera;
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void OnEnable()
        {
            if (SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
            {
                SetIsVrPlayerActive(isVrPlayerActive);
            }
            else
            {
                isVrPlayerActive = PlayerPrefs.GetString("CurrentPlayer").Equals("VR");
                UpdateActivePlayer();
            }
        }

        /// <summary>
        /// Updates the current active player according to the <see cref="isVrPlayerActive"/> state
        /// </summary>
        private void UpdateActivePlayer()
        {
            keyBoardPlayer.SetActive(!isVrPlayerActive);
            vrPlayer.SetActive(isVrPlayerActive);
        }

        /// <summary>
        /// Returns the current player game object, which is either the xrOrigin or the keyboardPlayer.
        /// This game object can be used to obtain the current player position, orientation, scale etc.
        /// </summary>
        public GameObject GetPlayerGameObject()
        {
            return isVrPlayerActive ? xrOrigin : keyBoardPlayer;
        }

        /// <summary>
        /// Returns the current player camera
        /// </summary>
        public Camera GetPlayerCamera()
        {
            return isVrPlayerActive ? vrCamera : keyboardCamera;
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
            PlayerPrefs.SetString("CurrentPlayer", isVrPlayerActive ? "VR" : "Keyboard");

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