using UnityEngine;

namespace Game.Tasks.ExampleTimerTask
{
    public class ExampleTaskFactory : GameTaskFactory
    {
        [SerializeField] private ExampleTimerGameTask exampleTimerTaskPrefab;
        
        public ExampleTaskFactory() : base(taskType: TaskType.Example)
        {
        }
        
        protected override GameTask CreateTask(Vector3 position)
        {
            // create a Prefab instance and get the example task component
            GameObject instance = Instantiate(exampleTimerTaskPrefab.gameObject, position, Quaternion.identity);
            ExampleTimerGameTask exampleTimerGameTask = instance.GetComponent<ExampleTimerGameTask>();

            return exampleTimerGameTask;
        }
    }
}