using UnityEngine;

namespace Game.Tasks.ExampleTimerTask
{
    public class ExampleTaskFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private ExampleTimerGameTask exampleTimerTaskPrefab;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            // create a Prefab instance and get the example task component
            GameObject instance = Instantiate(exampleTimerTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), Quaternion.identity);
            ExampleTimerGameTask exampleTimerGameTask = instance.GetComponent<ExampleTimerGameTask>();

            return exampleTimerGameTask;
        }
    }
}