using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    // class to create instances of the energycore-task
    public class EnergyCoreFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartEnergyCoreTask energyCorePrefab;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(energyCorePrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.transform.rotation);
            StartEnergyCoreTask energyCoreTimerTask = instance.GetComponent<StartEnergyCoreTask>();
            
            
            return energyCoreTimerTask;
        }
    }
}