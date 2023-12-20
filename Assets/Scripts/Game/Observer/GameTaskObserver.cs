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
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTaskObserver";
        
        // sound-manager-scripts to play sounds for tasks
        [SerializeField] private SoundManager taskFailureSoundManager;
        
        [SerializeField] private SoundManager taskSuccessSoundManager;
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            m_LOG.Log(LOGTag,"task successful: " + task.taskName);
            taskSuccessSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskFailed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task failed: " + task.taskName);
            taskFailureSoundManager.PlaySoundFunctionCall();
        }
        
        protected override void OnTaskGameObjectDestroyed(GameTask task)
        {
            base.OnTaskGameObjectDestroyed(task);
            m_LOG.Log(LOGTag,"task game object destroyed: " + task.taskName);
        }
    }
}