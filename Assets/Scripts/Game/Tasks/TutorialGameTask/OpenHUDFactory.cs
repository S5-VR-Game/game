using UnityEngine;

namespace Game.Tasks.TutorialGameTask
{
    /// <summary>
    /// This class is there to construct a task where the user
    /// has to open and close the HUD
    /// </summary>
    public class OpenHUDFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public GameObject hudTaskPrefab;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var instance = Instantiate(hudTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            var storageRiddleGameTask = instance.GetComponent<OpenHUDTask>();
            
            return storageRiddleGameTask;
        }
    }
}