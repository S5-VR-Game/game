using UnityEngine;

namespace Game.Tasks.ExampleMultipleObjectsTask
{
    /// <summary>
    /// Example Factory to spawn additional custom objects next to the task
    /// </summary>
    public class ExampleMultipleObjectsTaskFactory : GameTaskFactory<ExampleMultipleObjectsSpawnPoint>
    {
        [SerializeField] private ExampleMultipleObjectsTask exampleMultipleObjectsTaskPrefab;

        [Header("Prefabs")] [SerializeField] private GameObject customSpawnObject1Prefab;
        [SerializeField] private GameObject customSpawnObject2Prefab;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(ExampleMultipleObjectsSpawnPoint spawnPoint)
        {
            // create a Prefab instance and get the example task component
            GameObject instance =
                Instantiate(exampleMultipleObjectsTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), Quaternion.identity);
            ExampleMultipleObjectsTask exampleMultipleObjectsGameTask =
                instance.GetComponent<ExampleMultipleObjectsTask>();

            // spawn custom multiple objects
            exampleMultipleObjectsGameTask.AddLinkedGameObject(
                Instantiate(customSpawnObject1Prefab, spawnPoint.customSpawnPoint1.transform.position, Quaternion.identity)
            );
            
            foreach (var point in spawnPoint.customSpawnPointList)
            {
                exampleMultipleObjectsGameTask.AddLinkedGameObject(
                    Instantiate(customSpawnObject2Prefab, point.transform.position, Quaternion.identity)
                );
            }


            return exampleMultipleObjectsGameTask;
        }
    }
}