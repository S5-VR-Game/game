using System;
using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    // class used to handle the collision of asteroids and the wall
    public class CountSpaceStationHits : MonoBehaviour
    {
        public Difficulty difficulty; // stores the selected difficulty-value
        public SpawnAsteroids spawnAsteroidsScript; 
        
        public enum Difficulty // enum used to set the difficulty of the task
        {
            Easy,
            Medium,
            Hard
        }
        
        private int _maxHitCounter; // stores the value how often an asteroid can he the wall to lose the task
        private int _hitCounter; // stores the value how often an asteroid hit the wall
        
        [HideInInspector]
        public bool taskOverWall;// == true, when too many asteroids hit the wall 

        private void OnEnable()
        {
            SetupDifficulty();
            _hitCounter = 0;
        }

        // function to handle collision between asteroid and wall
        private void OnTriggerEnter(Collider other)
        {
            // checks if the colliding object has the tag "Asteroid"
            if (!other.gameObject.CompareTag("Asteroid")) return;

            // highers the counter up by 1 and destroys the asteroid
            _hitCounter++;
            Destroy(other.gameObject);
            
            if (_hitCounter >= _maxHitCounter) // checks if the task is lost
            {
                spawnAsteroidsScript.StopAsteroidSpawningLost();
                
                taskOverWall = true;
            }
            else if(_hitCounter < _maxHitCounter) // checks if the amount of hits on the wall are lower than the limit
            {
                Debug.Log("Hit! Hits left: " + (_maxHitCounter - _hitCounter));
            }
        }

        // sets the value of _maxHintCounter depending on the selected difficulty
        private void SetupDifficulty()
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    _maxHitCounter = 5;
                    Debug.Log("Max Hits: " + _maxHitCounter);
                    break;
                case Difficulty.Medium:
                    _maxHitCounter = 3;
                    Debug.Log("Max Hits: " + _maxHitCounter);
                    break;
                case Difficulty.Hard:
                    _maxHitCounter = 1;
                    Debug.Log("Max Hits: " + _maxHitCounter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
