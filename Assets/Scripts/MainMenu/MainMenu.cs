using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas canvas;

    public GameObject keyboard_player;
    public GameObject vr_player;

    public GameObject player_profile;

    private GameObject play_button;
    private GameObject settings_button;
    private GameObject exit_button;

    private GameObject homeprofile_button;
    private GameObject vrprofile_button;
    private GameObject size;
    private GameObject back_button;

    public void Start()
    {
        play_button = GameObject.Find("PlayButton").gameObject;
        settings_button = GameObject.Find("SettingsButton").gameObject;
        exit_button = GameObject.Find("ExitButton").gameObject;

        homeprofile_button = GameObject.Find("HomeProfileButton").gameObject;
        vrprofile_button = GameObject.Find("VRProfileButton").gameObject;
        size = GameObject.Find("PlayerHeight").gameObject;
        back_button = GameObject.Find("BackButton").gameObject;

        homeprofile_button.SetActive(false);
        vrprofile_button.SetActive(false);
        size.SetActive(false);
        back_button.SetActive(false);

        if(PlayerPrefs.GetString("CurrentPlayer", "Keyboard").Equals("Keyboard")) 
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            keyboard_player.SetActive(true);
            vr_player.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (PlayerPrefs.GetString("CurrentPlayer", "VR").Equals("VR"))
        {
            canvas.renderMode = RenderMode.WorldSpace;
            keyboard_player.SetActive(false);
            vr_player.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ActivateSettings()
    {
        play_button.SetActive(false);
        settings_button.SetActive(false);
        exit_button.SetActive(false);
        homeprofile_button.SetActive(true);
        vrprofile_button.SetActive(true);
        size.SetActive(true);     
        back_button.SetActive(true);
    }

    public void DeactivateSettings()
    {
        play_button.SetActive(true);
        settings_button.SetActive(true);
        exit_button.SetActive(true);
        homeprofile_button.SetActive(false);
        vrprofile_button.SetActive(false);
        size.SetActive(false);
        back_button.SetActive(false);
    }

    public void ActivateKeyboardPlayer() 
    {
        vr_player.SetActive(false);
        keyboard_player.SetActive(true);
        PlayerPrefs.SetString("CurrentPlayer", "Keyboard");

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ActivateVRPlayer()
    {
        vr_player.SetActive(true);
        keyboard_player.SetActive(false);
        PlayerPrefs.SetString("CurrentPlayer", "VR");

        canvas.renderMode = RenderMode.WorldSpace;
    }
}
