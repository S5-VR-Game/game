using UnityEngine;

namespace Game.Tasks.GameExit
{
    /// <summary>
    /// Factory for game exit task. This factory needs no further logic for creating the task, because the game exit
    /// task has no specific logic eiter.
    /// </summary>
    public class GameExitTaskFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private GameExitTask gameExitMarkerPrefab;
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            return Instantiate(gameExitMarkerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}