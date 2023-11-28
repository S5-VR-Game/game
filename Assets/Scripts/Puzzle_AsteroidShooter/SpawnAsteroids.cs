using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzle_AsteroidShooter
{
    // method to handle the asteroid-spawning and moving to the player
    public class SpawnAsteroids : MonoBehaviour
    {
        public GameObject rock; // stores the prefab of the used asteroid
        public new GameObject camera; // stores the object of the used camera
        
        // possible positions for the asteroid to spawn
        public Transform spawnPos1;
        public Transform spawnPos2;
        public Transform spawnPos3;
        public Transform spawnPos4;
        public Transform spawnPos5;
        public Transform spawnPos6;
        public Transform spawnPos7;
        public Transform spawnPos8;
        private Transform[] _spawnPositions = new Transform[8];

        // possible positions the asteroid could fly to
        public Transform endPoint1;
        public Transform endPoint2;
        public Transform endPoint3;
        public Transform endPoint4;
        private Transform[] _endPoints = new Transform[4];
        
        public Difficulty difficulty; // stores the selected difficulty-value
        
        private float _spawnTime; // stores the time remaining until the next asteroid will spawn
        private float _playingTime; // stores the time this task is played
        
        private float _spawnInterval; // stores the value of the interval between two asteroid-spawns
        private float _projectileSpeed; // stores the speed of the projectile
        private float _gameTime; // stores the time this task should run
        
        private bool _running = true; // stores the value if the task is running
        
        [HideInInspector]
        public bool taskOverTime; // = true, when the time of the task is over
        
        // stores the possible difficulties of the task
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        
        private void OnEnable()
        {
            SetupDifficulty();

            // sets the default starting values
            _spawnTime = _spawnInterval;
            _playingTime = 0f;
            _running = true;
            
            _spawnPositions = new[] { spawnPos1, spawnPos2, spawnPos3, spawnPos4, spawnPos5, spawnPos6, spawnPos7, spawnPos8 };
            _endPoints = new[] { endPoint1, endPoint2, endPoint3, endPoint4 };
        }

        private void Update()
        {
            if (_running)
            {
                _spawnTime -= Time.deltaTime;

                if (_spawnTime <= 0)
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
                    newRock.GetComponent<Rigidbody>().velocity = direction * _projectileSpeed;

                    // interval of creating new asteroids
                    _spawnTime = _spawnInterval;
                    _playingTime += _spawnInterval;

                    // Check if the playing time exceeds the game time
                    if (_playingTime >= _gameTime)
                    {
                        _running = false;
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
            return _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        }

        // selects one random endpoint out of the array
        private Transform GetRandomEndPoint()
        {
            return _endPoints[Random.Range(0, _endPoints.Length)];
        }

        // sets up the values of _spawnInternval, _projectileSpeed, _gameTime depenmding on the difficulty
        private void SetupDifficulty()
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    _spawnInterval = 2.5f;
                    _projectileSpeed = 3f;
                    _gameTime = 20f;
                    Debug.Log("Interval: " + _spawnInterval + ", Speed: " + _projectileSpeed + ", GameTime: " + _gameTime);
                    break;
                case Difficulty.Medium:
                    _spawnInterval = 2f;
                    _projectileSpeed = 4f;
                    _gameTime = 25f;
                    Debug.Log("Interval: " + _spawnInterval + ", Speed: " + _projectileSpeed + ", GameTime: " + _gameTime);
                    break;
                case Difficulty.Hard:
                    _spawnInterval = 1.5f;
                    _projectileSpeed = 5f;
                    _gameTime = 30f;
                    Debug.Log("Interval: " + _spawnInterval + ", Speed: " + _projectileSpeed + ", GameTime: " + _gameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // function used to stop the task if too many asteroid hitted the wall.
        public void StopAsteroidSpawningLost()
        {
            Debug.Log("You lost because too many asteroids hit the wall!");
            _running = false;
        }
    }
}