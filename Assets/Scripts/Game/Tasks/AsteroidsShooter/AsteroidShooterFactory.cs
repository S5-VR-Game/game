using PlayerController;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    public class AsteroidShooterFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartAsteroidShooter asteroidShooterTaskPrefab;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private Transform controllerTransform;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            StartAsteroidShooter asteroidShooterGameTask = instance.GetComponent<StartAsteroidShooter>();
            asteroidShooterGameTask.playerProfileService = playerProfileService;
            asteroidShooterGameTask.crosshairMouseMovement.controllerTransform = controllerTransform;
            asteroidShooterGameTask.crosshairMouseMovement.playerProfileService = playerProfileService;
            
            return asteroidShooterGameTask;
        }
    }
}