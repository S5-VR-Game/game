using Game.Observer;
using Game.Tasks;
using Game.Tasks.TutorialGameTask;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Proceeds to the next tutorial state when a tutorial task is successfully completed.
    /// </summary>
    public class TutorialGameTaskObserver : GameTaskObserver
    {
        [SerializeField] private TutorialProcedure tutorialProcedure;
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            base.OnTaskSuccessful(task);
            // if a tutorial task is successful completed, change to next tutorial state
            if (task is TutorialGameTask or OpenHUDTask)
            {
                tutorialProcedure.OnTutorialTaskSuccessful();
            }
        }
    }
}