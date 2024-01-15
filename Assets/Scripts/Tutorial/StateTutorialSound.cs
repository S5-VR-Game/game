using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Invokes sound playback when the tutorial procedure is in the given state <see cref="tutorialState"/>.
    /// </summary>
    public class StateTutorialSound : TutorialSound
    {
        [SerializeField] private TutorialState tutorialState;

        protected override void Start()
        {
            base.Start();
            FindObjectOfType<TutorialProcedure>().OnTutorialStateChanged += state =>
            {
                // if tutorial state equals the defined state, request sound playback
                if (state == tutorialState)
                {
                    RequestSoundPlayBack();
                }
            };
        }
    }
}