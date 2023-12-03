using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class constructs a MarbleGravity instance
    /// only in given spawn points with transform and rotation.
    /// </summary>
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public MarbleGravity marbleGravity;
        [FormerlySerializedAs("_difficulty")] [SerializeField] private Difficulty difficulty;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(marbleGravity.gameObject, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<MarbleGravity>();
            return task;
        }
    }
}