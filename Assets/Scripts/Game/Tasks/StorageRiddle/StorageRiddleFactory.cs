using PlayerController;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class AsteroidShooterFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private GameObject storageRiddleGameObject;
        [SerializeField] private PlayerProfileService playerProfileService;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(storageRiddleGameObject.gameObject, spawnPoint.GetSpawnPosition(), new Quaternion());
            StartStorageRiddle storageRiddleGameTask = instance.GetComponent<StartStorageRiddle>();
            
            return storageRiddleGameTask;
        }
    }
}