using System;
using Game.Observer;
using Game.Tasks;
using Logging;
using PlayerController;
using Sound;
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

        [Header("Timer settings")] 
        
        [SerializeField] private float initialGameTime = 60;
        [SerializeField] private float difficultyTimeModifier = 10;
        [SerializeField] private float minTimeIntervalBetweenTasks = 10;
        
        // determines the size of the interval from which a random value is used for the next game task time
        // higher values will result in a greater chance of more widely spread time intervals
        [SerializeField] private float randomTimeIntervalSize = 15;
        
        [SerializeField] private float taskSpawnPointTimeout = 10;
        [SerializeField] private float concurrentTasksLimit = 5;

        [Header("Game dependencies")] 
        [SerializeField] private Difficulty difficulty;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private GameTaskObserver gameTaskObserver;
        [SerializeField] private IntegrityObserver integrityObserver;
        
        [SerializeField] private GeneralGameTaskFactory[] factories;

        [SerializeField] private SoundManager _taskSpawningSoundManager;
        
        private float m_NextGameTaskTime; // if this time is reached, a new game task starts

        /// <summary>
        /// Event action, which is invoked, when the game timer reaches zero and the game ends 
        /// </summary>
        public event Action OnTimeOver;

        /// <summary>
        /// Event action, which is invoked, when the game timer changed.
        /// The float parameter represents the remaining time in seconds.
        /// </summary>
        public event Action<float> OnTimeChanged;

        public bool timerPaused { get; private set; }
        public float remainingTime { get; private set; }
        public bool timeOver { get; private set; }

        private void Start()
        {
            remainingTime = initialGameTime;
            m_NextGameTaskTime = GetNextTimeInterval();

            // initialize factories
            var factoryInitializationData = new FactoryInitializationData(difficulty, playerProfileService,
                gameTaskObserver, integrityObserver, taskSpawnPointTimeout);
            foreach (var factory in factories)
            {
                factory.Initialize(factoryInitializationData);
            }
        }

        private void Update()
        {
            if (remainingTime > 0)
            {
                if (!timerPaused)
                {
                    remainingTime -= Time.deltaTime;
                    OnTimeChanged?.Invoke(remainingTime);

                    // check whether new game task time is reached
                    if (remainingTime < m_NextGameTaskTime)
                    {
                        // check if task limit is not reached yet
                        if (gameTaskObserver.GetActiveTaskCount() < concurrentTasksLimit)
                        {
                            TrySpawnRandomTask();
                        }
                        else
                        {
                            m_LOG.Log(LOGTag, "concurrent task limit reached, no task was spawned");
                        }
                        // update next game task time
                        m_NextGameTaskTime = GetNextTimeInterval();
                    }
                }
            }
            else if (!timeOver)
            {
                remainingTime = 0;
                timeOver = true;
                OnTimeOver?.Invoke();
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
                    _taskSpawningSoundManager.PlaySoundFunctionCall();
                    gameTaskObserver.IncrementActiveTask();
                    return;
                }
            }
            m_LOG.Log(LOGTag, "all spawn points of all factories are occupied, no task was spawned");
        }

        /// <summary>
        /// Calculates the time until next game task starts according to the current remaining time 
        /// </summary>
        /// <returns>time value in seconds, at which a new game task should start</returns>
        private float GetNextTimeInterval()
        {
            // determine time interval to next game task start 
            var timeIntervalStart = difficultyTimeModifier * (1f - difficulty.GetValue());

            var timeIntervalEnd = timeIntervalStart + randomTimeIntervalSize;
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