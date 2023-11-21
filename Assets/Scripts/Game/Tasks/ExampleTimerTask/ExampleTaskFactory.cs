using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tasks.ExampleTimerTask
{
    public class ExampleTaskFactory : GameTaskFactory
    {
        [FormerlySerializedAs("exampleTaskPrefab")] [SerializeField] private ExampleTimerGameTask exampleTimerTaskPrefab;
        
        protected override GameTask CreateTask(Vector3 position)
        {
            // create a Prefab instance and get the example task component
            GameObject instance = Instantiate(exampleTimerTaskPrefab.gameObject, position, Quaternion.identity);
            ExampleTimerGameTask exampleTimerGameTask = instance.GetComponent<ExampleTimerGameTask>();

            return exampleTimerGameTask;
        }
    }
}