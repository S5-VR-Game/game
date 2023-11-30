using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    public class MixingIngredientsFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private MixingIngredients mixingIngredientsPrefab;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(mixingIngredientsPrefab.gameObject, spawnPoint.GetSpawnPosition(), Quaternion.identity);
            MixingIngredients exampleTimerGameTask = instance.GetComponent<MixingIngredients>();

            return exampleTimerGameTask;
        }
    }
}