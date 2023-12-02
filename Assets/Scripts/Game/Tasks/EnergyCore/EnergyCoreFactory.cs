using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartEnergyCoreTask asteroidShooterTaskPrefab;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), new Quaternion());
            StartEnergyCoreTask energyCoreTimerTask = instance.GetComponent<StartEnergyCoreTask>();
            
            
            return energyCoreTimerTask;
        }
    }
}