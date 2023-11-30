using System;
using Logging;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to handle the collision of asteroids and the wall
    public class CountSpaceStationHits : MonoBehaviour
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "CountSpaceStationHits";
        
        public Difficulty difficulty; // stores the selected difficulty-value
        public SpawnAsteroids spawnAsteroidsScript; 
        
        private int maxHitCounter; // stores the value how often an asteroid can he the wall to lose the task
        private int hitCounter; // stores the value how often an asteroid hit the wall
        
        [HideInInspector]
        public bool taskOverWall;// == true, when too many asteroids hit the wall 

        private void Start()
        {
            SetupDifficulty();
            hitCounter = 0;
        }

        // function to handle collision between asteroid and wall
        private void OnTriggerEnter(Collider other)
        {
            // checks if the colliding object has the tag "Asteroid"
            if (!other.gameObject.CompareTag("Asteroid")) return;

            // highers the counter up by 1 and destroys the asteroid
            hitCounter++;
            Destroy(other.gameObject);
            
            if (hitCounter >= maxHitCounter) // checks if the task is lost
            {
                spawnAsteroidsScript.StopAsteroidSpawningLost();
                
                taskOverWall = true;
            }
            else if(hitCounter < maxHitCounter) // checks if the amount of hits on the wall are lower than the limit
            {
                m_LOG.Log(LOGTag, "Hit! Hits left: " + (maxHitCounter - hitCounter));
            }
        }

        // sets the value of _maxHintCounter depending on the selected difficulty
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    maxHitCounter = 5;
                    m_LOG.Log(LOGTag,"Max Hits: " + maxHitCounter);
                    break;
                case SeparatedDifficulty.Medium:
                    maxHitCounter = 3;
                    m_LOG.Log(LOGTag,"Max Hits: " + maxHitCounter);
                    break;
                case SeparatedDifficulty.Hard:
                    maxHitCounter = 1;
                    m_LOG.Log(LOGTag,"Max Hits: " + maxHitCounter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
