using UnityEngine;

namespace Game.Tasks
{
    /// <summary>
    /// Organizes a spawn point to keep track of the current state, whether it is occupied or not.
    /// As this script extends from <see cref="MonoBehaviour"/>, the position of the according game object can serve
    /// as the spawn point position. The position vector can be obtained via <see cref="GetSpawnPosition"/>
    /// </summary>
    public class TaskSpawnPoint : MonoBehaviour
    {
        public bool isOccupied { get; private set; }

        /// <summary>
        /// Marks the spawn point as occupied and registers the <see cref="GameTask.GameObjectDestroyed"/> action to be
        /// observed by the <see cref="Deallocate"/> function. This allows this spawn point to be automatically
        /// deallocated, when the game task object is destroyed using the <see cref="GameTask.DestroyTask"/> method.
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
        public void Deallocate(GameTask gameTask)
        {
            Debug.Log("Deallocated gameTask");
            isOccupied = false;
            Debug.Log("isOccupied: " + isOccupied);
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