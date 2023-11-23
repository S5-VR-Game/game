using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        size = GameObject.Find("Size").gameObject;
        back_button = GameObject.Find("BackButton").gameObject;

        homeprofile_button.SetActive(false);
        vrprofile_button.SetActive(false);
        size.SetActive(false);
        back_button.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
