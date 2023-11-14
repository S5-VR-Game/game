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
        
        protected bool isCompleted;
        public event Action<GameTask> TaskCompleted;
        public event Action<GameTask> GameObjectDestroyed;

        /// <summary>
        /// Initializes the game task. Called when the game task is created.
        /// </summary>
        public abstract void Initialize();
        
        /// <summary>
        /// Notifies the observer, that this task is completed and changes the internal completed state
        /// </summary>
        protected void CompleteTask()
        {
            isCompleted = true;
            TaskCompleted?.Invoke(this);
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
