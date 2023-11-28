using Game.Tasks;
using Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    /// <summary>
    /// Game timer, that determines, when the game is over. It starts new tasks in a random time interval, that can
    /// be adjusted using the 'timer settings' fields.
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTimer";

        [SerializeField] public GeneralGameTaskFactory[] factories;

        [Header("Timer settings (every value in seconds)")] [SerializeField]
        public float initialGameTime = 60;

        [SerializeField] public float difficultyTimeModifier = 10;
        [SerializeField] public float minTimeIntervalBetweenTasks = 10;

        // determines the size of the interval from which a random value is used for the next game task time
        // higher values will result in a greater chance of more widely spread time intervals
        [SerializeField] public float randomTimeIntervalSize = 15;

        // higher difficulty value will result in higher chance to have less time between game task start times
        [Header("Game settings")] [SerializeField] [Range(0.0f, 1f)]
        public float difficulty = 0.3f;

        private float m_RemainingTime;
        private float m_NextGameTaskTime; // if this time is reached, a new game task starts
        private bool m_TimerPaused;
        private bool m_GameOver;

        private void Start()
        {
            m_RemainingTime = initialGameTime;
            m_NextGameTaskTime = GetNextTimeInterval();
        }

        private void Update()
        {
            if (m_RemainingTime > 0)
            {
                if (!m_TimerPaused)
                {
                    m_RemainingTime -= Time.deltaTime;

                    // check whether new game task is reached
                    if (m_RemainingTime < m_NextGameTaskTime)
                    {
                        TrySpawnRandomTask();

                        // update next game task time
                        m_NextGameTaskTime = GetNextTimeInterval();
                    }
                }
            }
            else if (!m_GameOver)
            {
                m_RemainingTime = 0;
                m_GameOver = true;
                m_LOG.Log(LOGTag, "game over");
            }
        }

        /// <summary>
        /// Tries to spawn a task by looping through a random order of factories and try if any factory can spawn a task
        /// </summary>
        private void TrySpawnRandomTask()
        {
            // shuffle factories in order to spawn random task type
            factories.Shuffle();
            
            // try to spawn a task and abort loop if succeeded
            foreach (var factory in factories)
            { 
                bool spawnSuccess = factory.TrySpawnTask();
                if (spawnSuccess)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Calculates the time until next game task starts according to the current remaining time 
        /// </summary>
        /// <returns>time value in seconds, at which a new game task should start</returns>
        private float GetNextTimeInterval()
        {
            // determine time interval to next game task start 
            var timeIntervalStart = difficultyTimeModifier * (1f - difficulty);

            var timeIntervalEnd = timeIntervalStart + randomTimeIntervalSize;
            //  random value between interval start and end
            return m_RemainingTime - minTimeIntervalBetweenTasks - Random.Range(timeIntervalStart, timeIntervalEnd);
        }

        /// <summary>
        /// Pauses the game timer
        /// </summary>
        public void PauseTimer()
        {
            m_TimerPaused = true;
        }

        /// <summary>
        /// Resumes the game timer
        /// </summary>
        public void ResumeTimer()
        {
            m_TimerPaused = false;
        }
    }
}