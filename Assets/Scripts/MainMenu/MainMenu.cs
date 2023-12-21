using PlayerController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public Canvas canvas;

        public GameObject playButton;
        public GameObject settingsButton;
        public GameObject exitButton;

        public GameObject height;
        public GameObject backButton;

        public PlayerProfileService playerProfileService;
        
        private void Start()
        {
            SetupMainMenu();
            playerProfileService.SetVRMovementActive(false);
        }

        // function to change the render-mode depending on the selected profile
        // if keyboard profile is selected: activates the mouse
        private void SetupMainMenu()
        {
            if (PlayerPrefs.GetString("CurrentPlayer", "").Equals("Keyboard"))
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (PlayerPrefs.GetString("CurrentPlayer", "").Equals("VR"))
            {
                canvas.renderMode = RenderMode.WorldSpace;
            }

            // disables the settings-buttons
            height.SetActive(false);
            backButton.SetActive(false);
        }

        public void PlayGame()
        {
            var cameraDirection = playerProfileService.GetPlayerCamera().transform.rotation.y;
            PlayerPrefs.SetFloat("CameraDirection", cameraDirection);
            
            //loads the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            // ends the application in the editor
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        // function needed when you need to change a setting
        public void ActivateSettings()
        {
            // enable and disable of the needed and not needed buttons
            playButton.SetActive(false);
            settingsButton.SetActive(false);
            exitButton.SetActive(false);
            height.SetActive(true);
            backButton.SetActive(true);
        }

        // function needed when you want to close the settings
        public void DeactivateSettings()
        {
            // enable and disable of the needed and not needed buttons
            playButton.SetActive(true);
            settingsButton.SetActive(true);
            exitButton.SetActive(true);
            height.SetActive(false);
            backButton.SetActive(false);
        }

    }
}
