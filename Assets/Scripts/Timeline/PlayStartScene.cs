using UnityEngine;
using UnityEngine.Playables;

public class PlayStartScene : MonoBehaviour
{

    public PlayableDirector timeline_director;
    public GameObject scene;

    // Start is called before the first frame update
    private void Start()
    {
        timeline_director.Play();
        timeline_director.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        scene.SetActive(false);
    }



}
