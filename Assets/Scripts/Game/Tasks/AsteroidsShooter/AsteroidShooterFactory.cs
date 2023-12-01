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
        [SerializeField] private XRNode rightController;
        [SerializeField] private XRNode leftController;
        [SerializeField] private bool useRightController = true;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            StartAsteroidShooter asteroidShooterGameTask = instance.GetComponent<StartAsteroidShooter>();
            asteroidShooterGameTask.playerProfileService = playerProfileService;
            asteroidShooterGameTask.crosshairMouseMovement.controller = GetControllerSide();
            
            return asteroidShooterGameTask;
        }

        private XRNode GetControllerSide()
        {
            return useRightController ? rightController : leftController;
        }
    }
}