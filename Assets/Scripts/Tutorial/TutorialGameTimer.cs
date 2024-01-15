using System;
using Game;
using Game.Tasks.TutorialGameTask;

namespace Tutorial
{
    /// <summary>
    /// Game timer specifically designed for the tutorial game. Modifies some timer behaviour to fit the tutorial.
    /// </summary>
    public class TutorialGameTimer : GameTimer
    {
        private const int OpenHUDTaskFactoryIndex = 0;
        private const int TutorialGameTaskFactoryIndex = 1;
        private const int TutorialTaskCount = 2;

        protected override void Start()
        {
            // check if factories are setup correctly
            if (factories.Length != TutorialTaskCount 
                || factories[OpenHUDTaskFactoryIndex] is not OpenHUDFactory 
                || factories[TutorialGameTaskFactoryIndex] is not TutorialGameTaskFactory)
            {
                throw new Exception(
                    $"TutorialGameTimer: Factories setup incorrectly. OpenHUDTaskFactory must be at index {OpenHUDTaskFactoryIndex} and TutorialGameTaskFactory must be at index {TutorialGameTaskFactoryIndex}.");
            }
            // initialize factories using the base method
            base.Start();
            // disable automatically spawning new tasks
            SetTaskSpawningEnabled(false);
        }

        /// <summary>
        /// Tries to spawn the tutorial task.
        /// </summary>
        public void SpawnTutorialTask()
        {
            factories[TutorialGameTaskFactoryIndex].TrySpawnTask(evaluationDataWrapper);
        }

        /// <summary>
        /// Tries to spawn the hud task.
        /// </summary>
        public void SpawnOpenHUDTask()
        {
            factories[OpenHUDTaskFactoryIndex].TrySpawnTask(evaluationDataWrapper);
        }
    }
}