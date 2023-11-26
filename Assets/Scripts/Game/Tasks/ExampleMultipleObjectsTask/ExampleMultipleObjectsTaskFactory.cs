using System;
using UnityEngine;

namespace Game.Tasks.ExampleMultipleObjectsTask
{
    public class ExampleMultipleObjectsTaskFactory : GameTaskFactory
    {
        [SerializeField] private ExampleMultipleObjectsTask exampleMultipleObjectsTaskPrefab;

        [Header("Points")] [SerializeField] private GameObject customSpawnPoint1;
        [SerializeField] private GameObject[] customSpawnPointList;

        [Header("Prefabs")] [SerializeField] private GameObject customSpawnObject1Prefab;
        [SerializeField] private GameObject customSpawnObject2Prefab;


        public ExampleMultipleObjectsTaskFactory() : base(taskType: TaskType.ExampleMultipleObjects)
        {
        }

        protected override GameTask CreateTask(Vector3 position)
        {
            // create a Prefab instance and get the example task component
            GameObject instance =
                Instantiate(exampleMultipleObjectsTaskPrefab.gameObject, position, Quaternion.identity);
            ExampleMultipleObjectsTask exampleMultipleObjectsGameTask =
                instance.GetComponent<ExampleMultipleObjectsTask>();

            // spawn custom multiple objects
            exampleMultipleObjectsGameTask.customSpawnedObjects.Add(
                Instantiate(customSpawnObject1Prefab, customSpawnPoint1.transform.position, Quaternion.identity)
            );
            foreach (var spawnPoint in customSpawnPointList)
            {
                exampleMultipleObjectsGameTask.customSpawnedObjects.Add(
                    Instantiate(customSpawnObject2Prefab, spawnPoint.transform.position, Quaternion.identity)
                );
            }


            return exampleMultipleObjectsGameTask;
        }
    }
}