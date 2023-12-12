using PlayerController;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline
{
    public class PlayStartScene : MonoBehaviour
    {
        // PlayableDirector is needed for the timeline to run
        public PlayableDirector timelineDirector;

        public PlayerProfileService playerProfileService;

        public Transform startScenePlayerPosition;

        // Start is called before the first frame update
        private void Start()
        {
            playerProfileService.SetVRMovementActive(false);
            
            playerProfileService.GetPlayerGameObject().transform.position = startScenePlayerPosition.position;
            
            playerProfileService.GetHUD().gameObject.SetActive(false);
            
            // starts the start-scene
            timelineDirector.Play();
            // calls the function when the timeline is over
            timelineDirector.stopped += OnTimelineStopped;
        }

        private void Update()
        {
            if (!playerProfileService.GetIsVrPlayerActive())
            {
                playerProfileService.GetPlayerGameObject().transform.position = startScenePlayerPosition.position;
            }
        }

        // is called when the timeline is over
        private void OnTimelineStopped(PlayableDirector director)
        {
            playerProfileService.SetVRMovementActive(true);
            
            playerProfileService.GetPlayerGameObject().transform.position = new Vector3(0, 2, 0);
            playerProfileService.GetHUD().gameObject.SetActive(true);
            
            gameObject.SetActive(false);
        }



    }
}