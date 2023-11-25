using UnityEngine;
using UnityEngine.Playables;

namespace Timeline
{
    public class PlayStartScene : MonoBehaviour
    {
        // PlayableDirector is needed for the timeline to run
        public PlayableDirector timelineDirector;
        
        // store the sceme
        public GameObject scene;

        // Start is called before the first frame update
        private void Start()
        {
            // starts the start-scene
            timelineDirector.Play();
            // calls the function when the timeline is over
            timelineDirector.stopped += OnTimelineStopped;
        }

        // is called when the timeline is over
        private void OnTimelineStopped(PlayableDirector director)
        {
            scene.SetActive(false);
        }



    }
}