using Game.Observer;
using Logging;
using UnityEngine;

namespace Game.Tasks
{
    
    /// <summary>
    /// Non generic superclass for <see cref="GameTaskFactory{T}"/>, which provides abstract non generic structures.
    /// This class is necessary to define a non generic super-type to take in a collection of differently parameterized
    /// generic subclasses.
    /// This acts like the wildcard operator in Java (<code>List&lt;? extends Number&gt; </code>)
    /// 
    /// Example usage:
    /// <code>
    /// public GeneralGameTaskFactory[] factories;
    /// factories[0].TrySpawnTask();
    /// </code>
    /// </summary>
    public abstract class GeneralGameTaskFactory : MonoBehaviour
    {
        /// <summary>
        /// See <see cref="GameTaskFactory{T}.TrySpawnTask()"/> and class documentation
        /// <see cref="GeneralGameTaskFactory"/> for further documentation about why this method is not
        /// documented here directly.
        /// </summary>
        public abstract bool TrySpawnTask(Difficulty difficulty);
    }
    
    /// <summary>
    /// Abstract factory class for creating new game tasks
    /// </summary>
    /// <typeparam name="T">
    /// Spawn point class, which is uses to spawn individual game tasks for this factory
    /// Needs to inherit from <see cref="TaskSpawnPoint"/>.
    /// Use <see cref="TaskSpawnPoint"/> as generic type, if the task not requires any custom spawn data.
    /// </typeparam>
    public abstract class GameTaskFactory<T>: GeneralGameTaskFactory where T : TaskSpawnPoint
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "GameTaskFactory";
        
        [Header("Observer")]
        [SerializeField] public GameTaskObserver gameTaskObserver;
        [SerializeField] public IntegrityObserver integrityObserver;
        
        [SerializeField] public T[] spawnPoints;

        /// <summary>
        /// Tries to spawn a new game task at a random spawn point of the list <see cref="spawnPoints"/>. If all spawn
        /// points are currently occupied, the method will not create a game task and return false.
        /// Furthermore the new task is also registered to the observer instances.
        /// </summary>
        /// <returns>true, if a game task is spawned and false, if no task was created due to no available spawn points</returns>
        public override bool TrySpawnTask(Difficulty difficulty)
        {
            // shuffle list to get random spawn points
            spawnPoints.Shuffle();
            // search for non-occupied spawn point
            foreach (var spawnPoint in spawnPoints)
            {
                print("Spawn Point Occupied: " + spawnPoint.isOccupied);
                if (spawnPoint.isOccupied)
                {
                    continue;
                }
                
                // create task to spawn at this spawn point
                GameTask newTask = CreateTask(spawnPoint);
                newTask.difficulty = difficulty;
                
                // allocate spawn point with newly created task
                spawnPoint.Allocate(newTask);
            
                // initialize task with its own logic
                newTask.Initialize();
            
                // register the new task
                gameTaskObserver.RegisterGameTask(newTask);
                integrityObserver.RegisterGameTask(newTask);

                m_LOG.Log(LOGTag, "task spawned successfully");
                return true;
            }
            
            // all spawnPoint are occupied and no task could be spawned
            m_LOG.Log(LOGTag, "all spawn points are occupied, no task was spawned");
            return false;
        }

        /// <summary>
        /// Instantiates the new game task and all related data or objects, like particles, sounds, etc.
        /// This method should not call the <see cref="GameTask.Initialize"/> method, as the task is initialized in the
        /// <see cref="TrySpawnTask"/> method already. 
        ///
        /// Example code to override and implement this method and obtain the <see cref="GameTask"/> object, that should
        /// be returned from this method:
        /// <code>
        /// GameObject instance = Instantiate(taskPrefab.gameObject, spawnPoint.GetSpawnPosition(), Quaternion.identity);
        /// ExampleGameTask exampleGameTask = instance.GetComponent&lt;ExampleGameTask&gt;();
        /// </code>
        /// </summary>
        /// <param name="spawnPoint">Spawn point where the new created task should be created. The vector position can
        /// be obtained using the method <see cref="TaskSpawnPoint.GetSpawnPosition()"/> of the given spawn point.</param>
        /// <returns>GameTask object for this task</returns>
        protected abstract GameTask CreateTask(T spawnPoint);

    }
}
