using Game.Observer;
using UnityEngine;


namespace Game.Tasks
{
    
    /// <summary>
    /// Abstract factory class for creating new game tasks
    /// </summary>
    public abstract class GameTaskFactory: MonoBehaviour
    {
        [Header("Game task observer")]
        [SerializeField] public GameTaskObserver gameTaskObserver;
        [SerializeField] public IntegrityObserver integrityObserver;

        /// <summary>
        /// Creates a new task using the implemented <see cref="CreateTask"/> method, initialize and
        /// registers this task with the observer
        /// </summary>
        /// <param name="position">Position, where to place the game object for this new task</param>
        /// <returns>The created game task</returns>
        public GameTask GetNewTask(Vector3 position)
        {
            GameTask newTask = CreateTask(position);
            
            // initialize task with its own logic
            newTask.Initialize();
            
            // register the new task
            gameTaskObserver.RegisterGameTask(newTask);
            integrityObserver.RegisterGameTask(newTask);
            
            return newTask;
        }

        /// <summary>
        /// Instantiates the new game task and all related data or objects, like particles, sounds, etc.
        /// This method should not call the <see cref="GameTask.Initialize"/> method, as the task is initialized in the
        /// <see cref="GetNewTask"/> method already. 
        ///
        /// Example code from the official unity tutorial for factory method pattern, that is used to override and
        /// implement this method:
        /// <code>
        /// GameObject instance = Instantiate(taskPrefab.gameObject, position, Quaternion.identity);
        /// ExampleGameTask exampleGameTask = instance.GetComponent&lt;ExampleGameTask&gt;();
        /// </code>
        /// </summary>
        /// <param name="position">Position, where to place the game object for this new task</param>
        /// <returns>GameTask object for this task</returns>
        protected abstract GameTask CreateTask(Vector3 position);

    }
}
