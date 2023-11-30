using PlayerController;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    public class AsteroidShooterFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartAsteroidShooter asteroidShooterTaskPrefab;
        [SerializeField] private PlayerProfileService playerProfileService;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            StartAsteroidShooter asteroidShooterGameTask = instance.GetComponent<StartAsteroidShooter>();
            asteroidShooterGameTask.playerProfileService = playerProfileService;

            return asteroidShooterGameTask;
        }
    }
}