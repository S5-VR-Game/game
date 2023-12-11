using PlayerController;
using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class constructs a MarbleGravity instance
    /// only in given spawn points with transform and rotation.
    /// </summary>
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public PlayerProfileService playerProfileService;
        public MarbleGravity marbleGravity;
        [SerializeField] private Difficulty difficulty;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(marbleGravity.gameObject, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<MarbleGravity>();
            task.playerProfileService = playerProfileService;
            return task;
        }
    }
}