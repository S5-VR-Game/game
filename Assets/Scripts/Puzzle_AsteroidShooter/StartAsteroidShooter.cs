using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    // class used to control the asteroid-shooter
    // IMPORTANT: THIS SCRIPT BELONGS TO AN OBJECT OUTSIDE THE ASTEROID-SHOOTER-PREFAB AND SHOULD NEVER ATTACH
    // TO AN OBJECT THAT COULD BE DEACTIVATED!
    public class StartAsteroidShooter : MonoBehaviour
    {
        public GameObject asteroidShooterScene; // needs the prefab of the asteroid-shooter
        public SpawnAsteroids spawnAsteroidsScript; // needs the script SpawnAsteroids
        public CountSpaceStationHits countSpaceStationHitsScript; // needs the script CountSpaceStationHits
        
        public GameObject playerProfile; // ONLY FOR TESTING (TO DEACTIVATE THE PLAYER)
        
        private bool _started;
        
        private void Start()
        {
            asteroidShooterScene.SetActive(false); // deactivates the asteroid-shooter at the beginning
        }
        
        // function need to be called when the task should be started
        // TODO NEEDS MORE SETUP WHEN CONNECTED TO TASK-LOGIC
        private void StartTask()
        {
            asteroidShooterScene.SetActive(true);
            
            playerProfile.SetActive(false); // deactivates camera to change camera
            Cursor.lockState = CursorLockMode.None; // unlocks the camera-movement bug
        }

        private void Update()
        {
            // checks if the time is over and the player has hits left
            if (spawnAsteroidsScript.taskOverTime)
            {
                Debug.Log("WIN");
                // TODO SEND WIN TO LOGIC
                
                ResetFlags();
            }

            // checks if too many asteroids hit the wall and the player has not hits left
            if (countSpaceStationHitsScript.taskOverWall)
            {
                Debug.Log("LOSE");
                // TODO SEND FAIL TO LOGIC
                
                ResetFlags();
            }

            // Press P to Start it
            // TODO need to be removed after adding the TASK-LOGIC
            if (Input.GetKey(KeyCode.P) && !_started)
            {
                _started = true;
                Debug.Log("START!");
                StartTask();
            }
        }

        // resets the values for checking if the game is over and what the result is
        private void ResetFlags()
        {
            asteroidShooterScene.SetActive(false);
                
            spawnAsteroidsScript.taskOverTime = false;
            countSpaceStationHitsScript.taskOverWall = false;
            _started = false;
            
            // activates the player again after the task
            playerProfile.SetActive(true);
        }
    }
}
