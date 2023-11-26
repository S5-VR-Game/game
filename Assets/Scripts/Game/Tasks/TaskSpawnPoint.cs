using UnityEngine;

namespace Game.Tasks
{
    /// <summary>
    /// Organizes a spawn point to keep track of the current state, whether it is occupied or not.
    /// This script also provides a field to define, which tasks are allowed to spawn at this spawn point.
    /// </summary>
    public class TaskSpawnPoint : MonoBehaviour
    {
        /// <summary>
        /// Defines, which tasks should be allowed to allocate to this spawn point.
        /// The value(s) depends on the environment, where the spawn point is placed at.
        /// E.g. a spawn point in the laboratory may allow to spawn a chemical task, but may not allow to spawn a
        /// transportation task, as the contexts (laboratory and transportation) are not matching.
        /// </summary>
        [SerializeField] public TaskType[] allocatableTasks;

        public bool isOccupied { get; private set;  }
        
        /// <summary>
        /// Marks the spawn point as occupied and registers the <see cref="GameTask.GameObjectDestroyed"/> action to be observed by the <see cref="Deallocate"/> function
        /// </summary>
        /// <param name="gameTask">game task, which occupies this spawn point</param>
        public void Allocate(GameTask gameTask)
        {
            isOccupied = true;
            gameTask.GameObjectDestroyed += Deallocate;
        }
        
        /// <summary>
        /// Marks the spawn point as not-occupied.
        /// </summary>
        private void Deallocate(GameTask gameTask)
        {
            isOccupied = false;
        }

        /// <summary>
        /// Provides the position vector of this spawn point
        /// </summary>
        /// <returns>position vector where to spawn the game task</returns>
        public Vector3 GetSpawnPosition()
        {
            return transform.position;
        }
    }
}
