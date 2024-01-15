using System.Collections.Generic;
using System.Collections.Generic;
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
        
        private readonly List<GameTask> m_ActiveTasks = new();
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            m_LOG.Log(LOGTag,"task successful: " + task.taskName);
            evaluationDataWrapper.IncrementMapEntry(task, DictTypes.TaskWon);
            m_ActiveTasks.Remove(task);
            taskSuccessSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskFailed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task failed: " + task.taskName);
            evaluationDataWrapper.IncrementMapEntry(task, DictTypes.TaskFailed);
            m_ActiveTasks.Remove(task);
            taskFailureSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskGameObjectDestroyed(GameTask task)
        {
            base.OnTaskGameObjectDestroyed(task);
            m_LOG.Log(LOGTag,"task game object destroyed: " + task.taskName);
        }

        public override void RegisterGameTask(GameTask task)
        {
            base.RegisterGameTask(task);
            m_ActiveTasks.Add(task);
        }

        /// <summary>
        /// Returns the current active tasks. Active means, that the task is not <see cref="TaskState.Failed"/> or
        /// <see cref="TaskState.Successful"/>.
        /// </summary>
        /// <returns>all currently active tasks</returns>
        public List<GameTask> GetActiveTasks()
        {
            return m_ActiveTasks;
        }
    }
}