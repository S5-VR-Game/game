using PlayerController;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to control the asteroid-shooter
    // IMPORTANT: THIS SCRIPT BELONGS TO AN OBJECT OUTSIDE THE ASTEROID-SHOOTER-PREFAB AND SHOULD NEVER ATTACH
    // TO AN OBJECT THAT COULD BE DEACTIVATED!
    public class StartAsteroidShooter : GameTask
    {
        public GameObject asteroidShooterScene; // needs the prefab of the asteroid-shooter
        public SpawnAsteroids spawnAsteroidsScript; // needs the script SpawnAsteroids
        public CountSpaceStationHits countSpaceStationHitsScript; // needs the script CountSpaceStationHits
        public ShootProjectile shootProjectile;
        public CrosshairMouseMovement crosshairMouseMovement;
        public PlayerProfileService playerProfileService;
        
        private bool started;
        
        public StartAsteroidShooter() : base("AsteroidShooter", "AsteroidShooter description")
        {
        }

        public override void Initialize()
        {
            shootProjectile.camera = playerProfileService.GetPlayerCamera();
            crosshairMouseMovement.camera = playerProfileService.GetPlayerCamera();
            countSpaceStationHitsScript.difficulty = difficulty;
            spawnAsteroidsScript.difficulty = difficulty;

            asteroidShooterScene.SetActive(false); // deactivates the asteroid-shooter at the beginning
        }

        protected override void BeforeStateCheck()
        {
            // check if player is nearby
            if (!started && Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) < 5)
            {
                // activate shooter scene
                asteroidShooterScene.SetActive(true);
                started = true;
            }
        }

        protected override TaskState CheckTaskState()
        {
            // checks if the time is over and the player has hits left
            if (spawnAsteroidsScript.taskOverAmount)
            {
                return TaskState.Successful;
            }

            // checks if too many asteroids hit the wall and the player has not hits left
            if (countSpaceStationHitsScript.taskOverWall)
            {
                return TaskState.Failed;
            }

            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}
