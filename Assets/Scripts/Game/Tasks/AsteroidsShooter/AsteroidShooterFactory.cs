using PlayerController;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.AsteroidsShooter
{
    public class AsteroidShooterFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartAsteroidShooter asteroidShooterTaskPrefab;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private XRNode leftController;
        [SerializeField] private GameObject locomotiveSystemMove;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            StartAsteroidShooter asteroidShooterGameTask = instance.GetComponent<StartAsteroidShooter>();
            asteroidShooterGameTask.playerProfileService = playerProfileService;
            asteroidShooterGameTask.crosshairMouseMovement.controller = leftController;
            asteroidShooterGameTask.shootProjectile.controller = leftController;
            
            if(PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                asteroidShooterGameTask.locomotiveSystemMove = locomotiveSystemMove;
            }
            
            return asteroidShooterGameTask;
        }
    }
}