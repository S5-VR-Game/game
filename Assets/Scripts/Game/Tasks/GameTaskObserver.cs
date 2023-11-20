using MyPrefabs.Scripts.Game.Tasks;
using MyPrefabs.Scripts.Logging;
using UnityEngine;

namespace MyPrefabs.Scripts.Game
{
    /// <summary>
    /// Observer for game task actions. 
    /// </summary>
    public class GameTaskObserver : MonoBehaviour
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTaskObserver";

        /// <summary>
        /// Lets this observer listen to events of the given game task
        /// </summary>
        /// <param name="task">game task to observe</param>
        public void RegisterGameTask(GameTask task)
        {
            task.TaskFailed += OnTaskFailed;
            task.TaskSuccessful += OnTaskSuccessful;
            task.GameObjectDestroyed += OnTaskGameObjectDestroyed;
        }

        /// <summary>
        /// Stops this observer to listen to events from the given game task
        /// </summary>
        /// <param name="task">game task to stop observing</param>
        public void DeregisterGameTask(GameTask task)
        {
            task.TaskFailed -= OnTaskFailed;
            task.TaskSuccessful -= OnTaskSuccessful;
            task.GameObjectDestroyed -= OnTaskGameObjectDestroyed;
        }

        
        /// <summary>
        /// Called when a game task is completed
        /// </summary>
        /// <param name="task">completed game task</param>
        private void OnTaskSuccessful(GameTask task)
        {
            m_LOG.Log(LOGTag,"task successful: " + task.taskName);
        }
        
        /// <summary>
        /// Called when a game task is failed
        /// </summary>
        /// <param name="task">failed game task</param>
        private void OnTaskFailed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task failed: " + task.taskName);
        }

        /// <summary>
        /// Called when the game object for a game task is destroyed.
        /// Automatically call deregister for this game object since its script is removed along with the game object 
        /// </summary>
        /// <param name="task"></param>
        private void OnTaskGameObjectDestroyed(GameTask task)
        {
            m_LOG.Log(LOGTag,"task game object destroyed: " + task.taskName);
            DeregisterGameTask(task);
        }
    }
}