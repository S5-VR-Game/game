using PlayerController;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayEndscene : MonoBehaviour
{
    public PlayableDirector timelineDirector;

    public PlayerProfileService playerService;
    private float distance = 3f;

    void Start()
    {
       
        // setting to make sure, that the endscene is not played after creating the space shuttle gameobject
        if (timelineDirector != null)
        {
            timelineDirector.playOnAwake = false;
            timelineDirector.stopped += OnTimelineStopped;
        }
    }

    public void PlayTimeline()
    {
        // checks, if the timelinedirector and the space shuttle object is not null
        if (timelineDirector != null) { 

            // plays the timeline
            timelineDirector.Play();
        }
    }

    // way to start the endscene (NEED TO CHANGE AFTERWARS AFTER GETTING THE WIN CONDITION!)
    void Update()
    {
        if (Vector3.Distance(playerService.getPlayerGameObject().transform.position, transform.position) <= distance)
        {
            Debug.Log("Starting Endscene!");
            PlayTimeline();
        }
    }

    // returns to the main menu after playing the endscene
    private void OnTimelineStopped(PlayableDirector director)
    {
        SceneManager.LoadScene(0);
        if(PlayerPrefs.GetString("CurrentPlayer", "Keyboard").Equals("Keyboard")) 
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}