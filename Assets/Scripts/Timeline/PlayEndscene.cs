using Game;
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

        [SerializeField] private GameInformation gameInformation;

        private void Start()
        {
            if (timelineDirector == null) return;

            // setting to deactivate that the end-scene is played after its game object creation
            timelineDirector.playOnAwake = false;

            // activates the function OnTimelineStopped() after the timeline ends
            timelineDirector.stopped += OnTimelineStopped;
        }

        // way to start the endscene
        private void Update()
        {
            if (!(Vector3.Distance(playerService.GetPlayerGameObject().transform.position, transform.position) <=
                  Distance)) return;
            
            if (gameInformation.currentGameState == GameState.GameWon)
            {
                // starts the end-scene
                timelineDirector.Play();
            }
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