using MyPrefabs.Scripts.Game.Tasks;
using MyPrefabs.Scripts.Logging;
using UnityEngine;

namespace MyPrefabs.Scripts.Game.Observer
{
    /// <summary>
    /// Observer for game task actions. 
    /// </summary>
    public class GameTaskObserver : AbstractObserver
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTaskObserver";
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            m_LOG.Log(LOGTag,"task successful: " + task.taskName);
        }
        
        protected override void OnTaskFailed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task failed: " + task.taskName);
        }
        
        protected override void OnTaskGameObjectDestroyed(GameTask task)
        {
            base.OnTaskGameObjectDestroyed(task);
            m_LOG.Log(LOGTag,"task game object destroyed: " + task.taskName);
        }
    }
}