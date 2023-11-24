using UnityEngine;
using UnityEngine.Playables;

public class PlayEndscene : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public GameObject space_shuttle;

    public GameObject player;
    private float distance = 2f;

    void Start()
    {
        // deactivates the space shuttle before the start of the timeline
        if (space_shuttle != null)
        {
            space_shuttle.SetActive(false);
        }

        // setting to make sure, that the endscene is not played after creating the space shuttle gameobject
        if (timelineDirector != null)
        {
            timelineDirector.playOnAwake = false;
        }
        else
        {
            Debug.LogError("PlayableDirector nicht zugewiesen oder ist null. Weisen Sie ihn im Inspector zu.");
        }
    }

    public void PlayTimeline()
    {
        // checks, if the timelinedirector and the space shuttle object is not null
        if (timelineDirector != null && space_shuttle != null)
        {
            // activates the space shuttle before starting the timeline
            space_shuttle.SetActive(true);

            // plays the timeline
            timelineDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector oder zu animierendes GameObject nicht zugewiesen oder ist null. Weisen Sie sie im Inspector zu.");
        }
    }

    // way to start the endscene (NEED TO CHANGE AFTERWARS AFTER GETTING THE WIN CONDITION!)
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            PlayTimeline();
        }
    }
}
