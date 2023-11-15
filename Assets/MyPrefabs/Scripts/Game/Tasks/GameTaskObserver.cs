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
        private Logger log;
        private static string logTAG = "GameTaskObserver";
        
        void Start()
        {
            log = new Logger(new LogHandler());
        }

        /// <summary>
        /// Lets this observer listen to events of the given game task
        /// </summary>
        /// <param name="task">game task to observe</param>
        public void registerGameTask(GameTask task)
        {
            task.TaskFailed += OnTaskFailed;
            task.TaskSuccessful += OnTaskSuccessful;
            task.GameObjectDestroyed += OnTaskGameObjectDestroyed;
        }

        /// <summary>
        /// Stops this observer to listen to events from the given game task
        /// </summary>
        /// <param name="task">game task to stop observing</param>
        public void deregisterGameTask(GameTask task)
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
            log.Log(logTAG,"task successful: " + task.taskName);
        }
        
        /// <summary>
        /// Called when a game task is completed
        /// </summary>
        /// <param name="task">completed game task</param>
        private void OnTaskFailed(GameTask task)
        {
            log.Log(logTAG,"task failed: " + task.taskName);
        }

        /// <summary>
        /// Called when the game object for a game task is destroyed.
        /// Automatically call deregister for this game object since its script is removed along with the game object 
        /// </summary>
        /// <param name="task"></param>
        private void OnTaskGameObjectDestroyed(GameTask task)
        {
            log.Log(logTAG,"task game object destroyed: " + task.taskName);
            deregisterGameTask(task);
        }
    }
}