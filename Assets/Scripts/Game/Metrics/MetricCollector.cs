using Game.Tasks;
using PlayerController;
using UnityEngine;
using AbstractObserver = Game.Observer.AbstractObserver;

namespace Game.Metrics
{
    /// <summary>
    /// Collects all necessary metric values from the game and keeps track of them using the <see cref="MetricData"/>.
    /// </summary>
    public class MetricCollector : AbstractObserver
    {
        [SerializeField] private GameInformation gameInformation;
        [SerializeField] private Integrity integrity;
        [SerializeField] private Difficulty difficulty;
        [SerializeField] private GameTimer gameTimer;
        [SerializeField] private PlayerProfileService playerProfileService;

        private readonly MetricData m_MetricData = new();
        
        private float m_LastIntegrityValue;
        private float m_LastTaskSpawnTimeSeconds;
        private Vector3 m_LastPlayerPosition;
        private GameState m_LastGameState;
        
        /// <summary>
        /// Threshold for the player to walk the distance and not teleport.
        /// If the position difference is greater than this value, the player probably teleported and the distance
        /// is not counted.
        /// </summary>
        private const float MaxPlayerWalkDistanceThreshold = 5f;
        
        private void Start()
        {
            // register to events
            gameInformation.OnGameStateChanged += OnGameStateChanged;
            integrity.OnIntegrityChanged += OnIntegrityChanged;
            playerProfileService.GetHUD().uiTextBox.OnTextOpened += OnHudTaskDescriptionOpened;
            
            m_LastIntegrityValue = integrity.GetCurrentIntegrity();
            m_LastTaskSpawnTimeSeconds = gameTimer.remainingTime;
        }

        private void FixedUpdate()
        {
            // track player walked distance
            var distance = Vector3.Distance(m_LastPlayerPosition, playerProfileService.GetPlayerGameObject().transform.position);
            // ensure, that the player walked the distance and was not teleported
            if (distance < MaxPlayerWalkDistanceThreshold)
            {
                m_MetricData.AddValueToMetric(SingleValueMetric.WalkedDistance, distance);
            }
            m_LastPlayerPosition = playerProfileService.GetPlayerGameObject().transform.position;

            if (Input.GetKey(KeyCode.E))
            {
                m_MetricData.WriteToFile();
            }
        }

        /// <summary>
        /// Event listener for the hud text description open event. Increases the hud description open count metric value.
        /// </summary>
        private void OnHudTaskDescriptionOpened()
        {
            // increase hud description open count metric value
            m_MetricData.IncrementMetricValue(SingleValueMetric.HudTaskDescriptionOpenCount);
        }

        /// <summary>
        /// Event listener for the integrity changed event. Updates the integrity increase/decrease sum metric values.
        /// </summary>
        /// <param name="newIntegrity"></param>
        private void OnIntegrityChanged(int newIntegrity)
        {
            float integrityChange = newIntegrity - m_LastIntegrityValue;
            if (integrityChange > 0)
            {
                m_MetricData.AddValueToMetric(SingleValueMetric.IntegrityIncreaseSum, integrityChange);
            }
            else
            {
                m_MetricData.AddValueToMetric(SingleValueMetric.IntegrityDecreaseSum, -integrityChange);
            }
            m_LastIntegrityValue = newIntegrity;
        }

        /// <summary>
        /// Event listener for the game state changed event.
        /// Updates the final integrity, difficulty and game won metric values and writes the metrics to file if the
        /// game is over.
        /// </summary>
        /// <param name="gameState">new game state</param>
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Ongoing)
            {
                return;
            }

            // update integrity metric value manually because the last integrity
            // changed event is invoked after the game over event
            OnIntegrityChanged(integrity.GetCurrentIntegrity());
            
            // if game is over, set final integrity, difficulty and game won metric values and write metrics to file
            m_MetricData.SetMetric(SingleValueMetric.FinalIntegrity, integrity.GetCurrentIntegrity());
            m_MetricData.SetMetric(SingleValueMetric.GameWon, gameState == GameState.GameWon);
            m_MetricData.SetMetric(SingleValueMetric.Difficulty, difficulty.GetValue());
            m_MetricData.WriteToFile();
            
        }
        
        /// <summary>
        /// Overrides the base method to be able to change metrics according to new task spawns.
        /// </summary>
        /// <param name="task">new task</param>
        public override void RegisterGameTask(GameTask task)
        {
            base.RegisterGameTask(task);
            
            // add new task spawn to metric service
            m_MetricData.RegisterTaskSpawn(task.taskType);
            // add new "seconds between task spawn"-value 
            m_MetricData.AddSecondsBetweenTaskSpawnValue(m_LastTaskSpawnTimeSeconds - gameTimer.remainingTime);
            
            m_LastTaskSpawnTimeSeconds = gameTimer.remainingTime;
        }

        /// <summary>
        /// Updates the timer task remaining seconds metric value, if the given task is a timer task.
        /// </summary>
        /// <param name="task">finished game task, remaining seconds of this task will be used to update the metric</param>
        private void UpdateTimerTaskRemainingSecondsMetric(GameTask task)
        {
            // if the task is a timer task, add the remaining seconds value to the metric
            if (task is TimerTask timerTask)
            {
                m_MetricData.AddTimerTaskRemainingSecondsValue(timerTask.GetRemainingTime());
            }
        }

        protected override void OnTaskSuccessful(GameTask task)
        {
            UpdateTimerTaskRemainingSecondsMetric(task);
            // register task success increase successful tasks count metric value
            m_MetricData.RegisterTaskSuccess(task.taskType);
            m_MetricData.IncrementMetricValue(SingleValueMetric.SuccessfulTasksCount);
        }

        protected override void OnTaskFailed(GameTask task)
        {
            UpdateTimerTaskRemainingSecondsMetric(task);
            // register task failure increase failed tasks count metric value
            m_MetricData.RegisterTaskFailure(task.taskType);
            m_MetricData.IncrementMetricValue(SingleValueMetric.FailedTasksCount);
        }
    }
}