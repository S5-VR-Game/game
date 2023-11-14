using System;
using MyPrefabs.Scripts.Game.Tasks;
using MyPrefabs.Scripts.Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyPrefabs.Scripts.Game
{
    /// <summary>
    /// Game timer, that determines, when the game is over. It starts new tasks in a random time interval, that can
    /// be adjusted using the 'timer settings' fields.
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        private Logger log = new Logger(new LogHandler());
        private String logTAG = "GameTimer";
        
        [SerializeField] private GameTaskFactory[] factories;

        [Header("Timer settings (every value in seconds)")]
        [SerializeField] public float initialGameTime = 60;
        [SerializeField] public float difficultyTimeModifier = 10;
        [SerializeField] public float minTimeIntervalBetweenTasks = 10;
        
        // determines the size of the interval from which a random value is used for the next game task time
        // higher values will result in a greater chance of more widely spread time intervals
        [SerializeField] public float randomTimeIntervalSize = 15;
        
        // higher difficulty value will result in higher chance to have less time between game task start times
        [Header("Game settings")]
        [SerializeField][Range(0.0f, 1f)] public float difficulty = 0.3f;
        
        private float remainingTime;
        private float nextGameTaskTime; // if this time is reached, a new game task starts
        private bool timerPaused;
        private bool gameOver;

        private void Start()
        {
            remainingTime = initialGameTime;
            nextGameTaskTime = GetNextTimeInterval();
        }

        private void Update()
        {
            if (remainingTime > 0)
            {
                if (!timerPaused)
                {
                    remainingTime -= Time.deltaTime;

                    // check whether new game task is reached
                    if (remainingTime < nextGameTaskTime)
                    {
                        // start game using random factory at the position of this game object
                        GameTask task = factories[Random.Range(0, factories.Length)].GetNewTask(transform.position);
                        task.taskName += " at seconds: " + (int) remainingTime;
                    
                        // update next game task time
                        nextGameTaskTime = GetNextTimeInterval();
                    }
                }
            }
            else if (!gameOver)
            {
                remainingTime = 0;
                gameOver = true;
                log.Log(logTAG, "game over");
            }
        }
        
        /// <summary>
        /// Calculates the time until next game task starts according to the current remaining time 
        /// </summary>
        /// <returns>time value in seconds, at which a new game task should start</returns>
        private float GetNextTimeInterval()
        {
            // determine time interval to next game task start 
            float timeIntervalStart = difficultyTimeModifier * (1f - difficulty);
            
            float timeIntervalEnd = timeIntervalStart + randomTimeIntervalSize;
            //  random value between interval start and end
            return remainingTime - minTimeIntervalBetweenTasks - Random.Range(timeIntervalStart, timeIntervalEnd);
        }

        /// <summary>
        /// Pauses the game timer
        /// </summary>
        public void PauseTimer()
        {
            timerPaused = true;
        }

        /// <summary>
        /// Resumes the game timer
        /// </summary>
        public void ResumeTimer()
        {
            timerPaused = false;
        }
    }
}