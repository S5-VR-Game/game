using Evaluation;
using Game.Tasks;
using Logging;
using Sound;
using UnityEngine;

namespace Game.Observer
{
    /// <summary>
    /// Observer for game task actions. 
    /// </summary>
    public class GameTaskObserver : AbstractObserver
    {
        public EvaluationDataWrapper evaluationDataWrapper;
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTaskObserver";
        
        // sound-manager-scripts to play sounds for tasks
        [SerializeField] private SoundManager taskFailureSoundManager;
        
        [SerializeField] private SoundManager taskSuccessSoundManager;
        
        private int m_ActiveTasks;
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            m_LOG.Log(LOGTag,"task successful: " + task.taskName);
            evaluationDataWrapper.IncrementMapEntry(task, DictTypes.TaskWon);
            m_ActiveTasks--;
            taskSuccessSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskFailed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task failed: " + task.taskName);
            evaluationDataWrapper.IncrementMapEntry(task, DictTypes.TaskFailed);
            m_ActiveTasks--;
            taskFailureSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskGameObjectDestroyed(GameTask task)
        {
            base.OnTaskGameObjectDestroyed(task);
            m_LOG.Log(LOGTag,"task game object destroyed: " + task.taskName);
        }

        /// <summary>
        /// Returns the current active task count.
        /// </summary>
        /// <returns>the number of active tasks</returns>
        public int GetActiveTaskCount()
        {
            return m_ActiveTasks;
        }

        /// <summary>
        /// Increments the active task counter by 1. Should be called once every time a new task is spawned.
        /// </summary>
        public void IncrementActiveTask()
        {
            m_ActiveTasks++;
        }
    }
}