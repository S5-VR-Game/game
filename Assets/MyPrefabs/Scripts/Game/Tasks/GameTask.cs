using System;
using UnityEngine;

namespace MyPrefabs.Scripts.Game.Tasks
{
    
    /// <summary>
    /// Abstract game task, that provides basic game task logic and data fields for game tasks.
    /// In addition it defines methods and fields for notifying state changes using the observer pattern.
    /// </summary>
    public abstract class GameTask : MonoBehaviour
    {
        [SerializeField]
        [Delayed]
        public String taskName;

        protected TaskState currentTaskState;
        public event Action<GameTask> TaskSuccessful;
        public event Action<GameTask> TaskFailed;
        public event Action<GameTask> GameObjectDestroyed;

        /// <summary>
        /// Initializes the game task. Called when the game task is created.
        /// </summary>
        public abstract void Initialize();
        
        protected abstract TaskState CheckTaskState();

        /// <summary>
        /// Updates the TaskState and performs a completion if the task is not ongoing anymore
        /// </summary>
        /// <param name="taskState">the new state of the game</param>
        protected void UpdateTaskState(TaskState taskState)
        {
            currentTaskState = taskState;
            switch (currentTaskState)
            {
                case TaskState.Failed:
                    TaskFailed?.Invoke(this);
                    break;
                case TaskState.Successful:
                    TaskSuccessful?.Invoke(this);
                    break;
            }
        }

        /// <summary>
        /// Removes the game object and all its components from the scene
        /// </summary>
        protected void DestroyGameObject()
        {
            GameObjectDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
