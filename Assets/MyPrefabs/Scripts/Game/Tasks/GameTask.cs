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
        /// Notifies the observer, that this task is completed and changes the internal completed state
        /// </summary>
        /// <param name="checkedTaskState"></param>
        private void CompleteTask()
        {
            switch (this.currentTaskState)
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
        /// Updates the TaskState and performs a completion if the Task is not ongoing anymore
        /// </summary>
        /// <param name="taskState">the new State of the Game</param>
        protected void UpdateTask(TaskState taskState)
        {
            this.currentTaskState = taskState;
            if (currentTaskState != TaskState.Ongoing)
            {
                CompleteTask();
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
