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
        public GameObject endSceneWinSceneGameObject;
        public GameObject endSceneLoseSceneGameObject;

        // stores the playable director of each timeline
        private PlayableDirector _startSceneDirector;
        private PlayableDirector _endSceneLoseDirector;
        private PlayableDirector _endSceneWinDirector;
        
        // stores the player profile service
        public PlayerProfileService playerProfileService;
        
        // stores the position the player should be placed after scene
        private Vector3 _playerStartPosition;
        
        private void Start()
        {
            _playerStartPosition = playerProfileService.transform.position; // stores the start position of the player
            
            // gets each playable director in runtime
            _startSceneDirector = startSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneLoseDirector = endSceneLoseSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneWinDirector = endSceneWinSceneGameObject.GetComponent<PlayableDirector>();
        }
        
        
        // function to start the timeline at the start of the game
        public void PlayStartScene()
        {
            SetupPlayerBeforeTimeline(startSceneGameObject.transform.position);
            _startSceneDirector.Play(); // starts the timeline

            _startSceneDirector.stopped += SetupAfterTimeline; // adds event listener to call function when timeline is over
            startSceneGameObject.SetActive(false);
        }

        public void PlayEndSceneLose()
        {
            SetupPlayerBeforeTimeline(startSceneGameObject.transform.position);
            
            _endSceneLoseDirector.Play(); // starts the timeline

            _endSceneLoseDirector.stopped += SetupAfterTimeline; // adds event listener to call function when timeline is over
            SceneManager.LoadScene(0); // loads the main-menu
        }
        
        public void PlayEndSceneWin()
        {
            SetupPlayerBeforeTimeline(startSceneGameObject.transform.position);
            
            _endSceneWinDirector.Play(); // starts the timeline

            _endSceneWinDirector.stopped += SetupAfterTimeline; // adds event listener to call function when timeline is over
            SceneManager.LoadScene(0); // loads the main-menu
        }

        // function used to setup the player for the timeline
        private void SetupPlayerBeforeTimeline(Vector3 timelinePosition)
        {
            playerProfileService.transform.position = timelinePosition; // moves the player's position to the timeline
            playerProfileService.GetHUD().gameObject.SetActive(false); // deactivates the hud
            playerProfileService.SetVRMovementActive(false); // deactivates the movement
        }

        // function used to setup the player after the timeline
        private void SetupAfterTimeline(PlayableDirector playableDirector)
        {
            playerProfileService.transform.position = _playerStartPosition; // moves the player's position to its original position
            playerProfileService.SetVRMovementActive(true); // deactivates the hud
            playerProfileService.GetHUD().gameObject.SetActive(true); // deactivates the movement
        }
    }
}