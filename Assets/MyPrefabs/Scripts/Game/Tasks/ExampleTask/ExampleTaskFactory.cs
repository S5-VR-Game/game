using UnityEngine;

namespace MyPrefabs.Scripts.Game.Tasks.ExampleTask
{
    public class ExampleTaskFactory : GameTaskFactory
    {
        [SerializeField] private ExampleGameTask exampleTaskPrefab;
        
        protected override GameTask CreateTask(Vector3 position)
        {
            // create a Prefab instance and get the example task component
            GameObject instance = Instantiate(exampleTaskPrefab.gameObject, position, Quaternion.identity);
            ExampleGameTask exampleGameTask = instance.GetComponent<ExampleGameTask>();

            return exampleGameTask;
        }
    }
}