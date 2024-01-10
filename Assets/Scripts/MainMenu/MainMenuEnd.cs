using PlayerController;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuEnd: MonoBehaviour
    {
        public Canvas canvas;
        
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
        }

        public void QuitGame()
        {
            // ends the application in the editor
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
