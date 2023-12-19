using Game;
using PlayerController;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Timeline 
{
    public class TimelineManager : MonoBehaviour
    {
        // stores the game-objects of the used timelines 
        public GameObject startSceneGameObject;
        public GameObject endSceneLoseSceneGameObject;
        public GameObject endSceneWinSceneGameObject;
        
        // stores the position the player should be teleported to
        public Transform startScenePlayerTransform;
        public Transform endSceneLosePlayerTransform;
        public Transform endSceneWinPlayerTransform;

        // stores the playable director of each timeline
        private PlayableDirector _startSceneDirector;
        private PlayableDirector _endSceneLoseDirector;
        private PlayableDirector _endSceneWinDirector;
        
        // stores the player profile service
        public PlayerProfileService playerProfileService;

        public GameTimer gameTimer;
        
        private void Start()
        {
            // gets each playable director in runtime
            _startSceneDirector = startSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneLoseDirector = endSceneLoseSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneWinDirector = endSceneWinSceneGameObject.GetComponent<PlayableDirector>();
            
            PlayStartScene();
        }
        
        
        // function to start the timeline at the start of the game
        private void PlayStartScene()
        {
            SetupPlayerBeforeTimeline(startScenePlayerTransform.position);
            _startSceneDirector.Play(); // starts the timeline
            gameTimer.PauseTimer();

            _startSceneDirector.stopped += SetupAfterStartTimeline; // adds event listener to call function when timeline is over
        }

        public void PlayEndSceneLose()
        {
            SetupPlayerBeforeTimeline(endSceneLosePlayerTransform.position);
            
            _endSceneLoseDirector.Play(); // starts the timeline

            _endSceneLoseDirector.stopped += SetupAfterEndTimeline; // adds event listener to call function when timeline is over
        }
        
        public void PlayEndSceneWin()
        {
            SetupPlayerBeforeTimeline(endSceneWinPlayerTransform.position);
            
            _endSceneWinDirector.Play(); // starts the timeline

            _endSceneWinDirector.stopped += SetupAfterEndTimeline; // adds event listener to call function when timeline is over
        }

        // function used to setup the player for the timeline
        private void SetupPlayerBeforeTimeline(Vector3 timelinePosition)
        {
            playerProfileService.transform.position = timelinePosition; // moves the player's position to the timeline
            playerProfileService.GetHUD().GetComponent<Canvas>().enabled = false; // deactivates the hud
            playerProfileService.SetVRMovementActive(false); // deactivates the movement
        }

        // function used to setup the player after the end timeline
        private void SetupAfterEndTimeline(PlayableDirector playableDirector)
        {
            SceneManager.LoadScene(0);
        }
        
        // function used to setup the player after the timeline
        private void SetupAfterStartTimeline(PlayableDirector playableDirector)
        {
            playerProfileService.transform.position = new Vector3(0, -1f, 0); // moves the player's position to its original position
            playerProfileService.GetHUD().GetComponent<Canvas>().enabled = true; // activates the hud
            playerProfileService.SetVRMovementActive(true); // activates the movement
            gameTimer.ResumeTimer();
        }
    }
}