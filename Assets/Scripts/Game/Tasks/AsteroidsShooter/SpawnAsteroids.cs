using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.AsteroidsShooter
{
    // method to handle the asteroid-spawning and moving to the player
    public class SpawnAsteroids : MonoBehaviour
    {
        public GameObject rock; // stores the prefab of the used asteroid
        
        // possible positions for the asteroid to spawn
        public Transform spawnPos1;
        public Transform spawnPos2;
        public Transform spawnPos3;
        public Transform spawnPos4;
        public Transform spawnPos5;
        public Transform spawnPos6;
        public Transform spawnPos7;
        public Transform spawnPos8;
        private Transform[] spawnPositions = new Transform[8];

        // possible positions the asteroid could fly to
        public Transform endPoint1;
        public Transform endPoint2;
        public Transform endPoint3;
        public Transform endPoint4;
        private Transform[] endPoints = new Transform[4];
        
        public Difficulty difficulty; // stores the selected difficulty-value
        
        private float spawnTime; // stores the time remaining until the next asteroid will spawn
        private float playingTime; // stores the time this task is played
        
        private float spawnInterval; // stores the value of the interval between two asteroid-spawns
        private float projectileSpeed; // stores the speed of the projectile
        private float gameTime; // stores the time this task should run
        
        private bool running = true; // stores the value if the task is running
        
        [HideInInspector]
        public bool taskOverTime; // = true, when the time of the task is over
        private void Start()
        {
            SetupDifficulty();

            // sets the default starting values
            spawnTime = spawnInterval;
            playingTime = 0f;
            running = true;
            
            spawnPositions = new[] { spawnPos1, spawnPos2, spawnPos3, spawnPos4, spawnPos5, spawnPos6, spawnPos7, spawnPos8 };
            endPoints = new[] { endPoint1, endPoint2, endPoint3, endPoint4 };
        }

        private void Update()
        {
            if (running)
            {
                spawnTime -= Time.deltaTime;

                if (spawnTime <= 0)
                {
                    // creates a random startpoint to create the asteroid
                    // and a random endpoint where the asteroid should fly to
                    var randomStartPoint = GetRandomSpawnPosition();
                    var randomEndPoint = GetRandomEndPoint();

                    // calculates the distance 
                    var position = randomStartPoint.position;
                    var direction = CalculateDirection(position, randomEndPoint.position);
                    direction.Normalize();

                    // instantiates an asteroid at the random selected spawnpoint
                    // and changes its flying direction
                    var newRock = Instantiate(rock, position, new Quaternion());
                    newRock.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

                    // interval of creating new asteroids
                    spawnTime = spawnInterval;
                    playingTime += spawnInterval;

                    // Check if the playing time exceeds the game time
                    if (playingTime >= gameTime)
                    {
                        running = false;
                        taskOverTime = true;
                        Debug.Log("Time is over! Stopped spawning new asteroids");
                    }
                }
            }
        }

        // calculates the direction through the spawnpoint and the endpoint
        private static Vector3 CalculateDirection(Vector3 startPoint, Vector3 endPoint)
        {
            var direction = endPoint - startPoint;

            return direction;
        }
        
        // selects one random spawnpoint out of the array
        private Transform GetRandomSpawnPosition()
        {
            return spawnPositions[Random.Range(0, spawnPositions.Length)];
        }

        // selects one random endpoint out of the array
        private Transform GetRandomEndPoint()
        {
            return endPoints[Random.Range(0, endPoints.Length)];
        }

        // sets up the values of _spawnInternval, _projectileSpeed, _gameTime depenmding on the difficulty
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    spawnInterval = 2.5f;
                    projectileSpeed = 3f;
                    gameTime = 20f;
                    Debug.Log("Interval: " + spawnInterval + ", Speed: " + projectileSpeed + ", GameTime: " + gameTime);
                    break;
                case SeparatedDifficulty.Medium:
                    spawnInterval = 2f;
                    projectileSpeed = 4f;
                    gameTime = 25f;
                    Debug.Log("Interval: " + spawnInterval + ", Speed: " + projectileSpeed + ", GameTime: " + gameTime);
                    break;
                case SeparatedDifficulty.Hard:
                    spawnInterval = 1.5f;
                    projectileSpeed = 5f;
                    gameTime = 30f;
                    Debug.Log("Interval: " + spawnInterval + ", Speed: " + projectileSpeed + ", GameTime: " + gameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // function used to stop the task if too many asteroid hitted the wall.
        public void StopAsteroidSpawningLost()
        {
            Debug.Log("You lost because too many asteroids hit the wall!");
            running = false;
        }
    }
}