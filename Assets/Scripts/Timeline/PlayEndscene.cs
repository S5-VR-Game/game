using PlayerController;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Timeline
{
    public class PlayEndscene : MonoBehaviour
    {
        // PlayableDirector is needed for the timeline to run
        public PlayableDirector timelineDirector;

        public PlayerProfileService playerService;
        private const float Distance = 3f;

        private void Start()
        {
            if (timelineDirector == null) return;
            
            // setting to deactivate that the end-scene is played after its game object creation
            timelineDirector.playOnAwake = false;
            
            // activates the function OnTimelineStopped() after the timeline ends
            timelineDirector.stopped += OnTimelineStopped;
        }

        // way to start the endscene (NEED TO CHANGE AFTERWARS AFTER GETTING THE WIN CONDITION!)
        private void Update()
        {
            if (!(Vector3.Distance(playerService.getPlayerGameObject().transform.position, transform.position) <=
                  Distance)) return;
            // starts the end-scene
            timelineDirector.Play();
        }

        // returns to the main menu after playing the end-scene
        // activates the mouse if the current player-profile type is keyboard player
        private static void OnTimelineStopped(PlayableDirector director)
        {
            SceneManager.LoadScene(0);
            if (!PlayerPrefs.GetString("CurrentPlayer", "Keyboard").Equals("Keyboard")) return;
        
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}