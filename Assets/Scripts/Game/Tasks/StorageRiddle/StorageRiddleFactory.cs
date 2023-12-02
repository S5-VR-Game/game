using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class StorageRiddleFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private GameObject storageRiddleGameObject; // stores the game-object of the prefab 
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(storageRiddleGameObject.gameObject, spawnPoint.GetSpawnPosition(), new Quaternion());
            StartStorageRiddle storageRiddleGameTask = instance.GetComponent<StartStorageRiddle>();
            
            return storageRiddleGameTask;
        }
    }
}