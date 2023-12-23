using Game.Tasks;
using UnityEngine;

namespace Game.Observer
{
    public abstract class AbstractObserver : MonoBehaviour
    {
        /// <summary>
        /// Called when a game task is completed
        /// </summary>
        /// <param name="task">completed game task</param>
        protected abstract void OnTaskSuccessful(GameTask task);

        /// <summary>
        /// Called when a game task is failed
        /// </summary>
        /// <param name="task">failed game task</param>
        protected abstract void OnTaskFailed(GameTask task);

        /// <summary>
        /// Called when the game object for a game task is destroyed.
        /// Automatically call deregister for this game object since its script is removed along with the game object 
        /// </summary>
        /// <param name="task"></param>
        protected virtual void OnTaskGameObjectDestroyed(GameTask task)
        {
            DeregisterGameTask(task);
        }
        
        /// <summary>
        /// Lets this observer listen to events of the given game task
        /// </summary>
        /// <param name="task">game task to observe</param>
        public virtual void RegisterGameTask(GameTask task)
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
    }
}