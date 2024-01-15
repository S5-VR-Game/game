using UnityEngine;

namespace Game.Tasks.TutorialGameTask
{
    /// <summary>
    /// Creates a Tutorial Task
    /// </summary>
    public class TutorialGameTaskFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private GameObject tutorialTask; 
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var instance = Instantiate(tutorialTask.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            var storageRiddleGameTask = instance.GetComponent<TutorialGameTask>();
            
            return storageRiddleGameTask;
        }
    }
}