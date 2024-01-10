using PlayerController;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuEnd: MonoBehaviour
    {
        public Canvas canvas;
        
        public PlayerProfileService playerProfileService;

        public GameObject idText;
        
        private void Start()
        {
            SetupMainMenu();
            playerProfileService.SetVRMovementActive(false);
            
            SetIDText(PlayerPrefs.GetString("GameID", "Missing!"));
            Debug.Log(PlayerPrefs.GetString("GameID", "Missing!"));
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

        private void SetIDText(string id)
        {
            idText.GetComponent<TextMeshProUGUI>().SetText(id);
        }
    }
}
