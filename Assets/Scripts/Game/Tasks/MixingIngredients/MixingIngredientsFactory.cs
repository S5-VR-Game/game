using System;
using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Factory class for the mixing ingredients task. Spawns all necessary game objects for the task.
    /// </summary>
    public class MixingIngredientsFactory : GameTaskFactory<MixingIngredientsSpawnPoint>
    {
        [SerializeField] private MixingIngredients mixingIngredientsPrefab;
        [SerializeField] private RecipeText recipeTextPrefab;
        [SerializeField] private IngredientSpawner ingredientSpawnerPrefab;
        [SerializeField] private WateringLiquidSpawner wateringLiquidSpawnerPrefab;
        [SerializeField] private WateringDetection plantToWaterPrefab;

        protected override GameTask CreateTask(MixingIngredientsSpawnPoint spawnPoint)
        {            
            // check if enough containers spawn points are available
            if (spawnPoint.ingredientContainerSpawnPoints.Length < MixingIngredients.IngredientTypes.Length)
            {
                throw new ArgumentException("Not enough ingredient container spawn points for spawn point: " +
                                            spawnPoint.name);
            }
            
            // spawn main task
            MixingIngredients mixingIngredientsTask = Instantiate(mixingIngredientsPrefab, spawnPoint.GetSpawnPosition(),
                spawnPoint.GetRotation());

            // spawn recipe text
            RecipeText recipeText = Instantiate(recipeTextPrefab, spawnPoint.recipeSpawnPoint.position, spawnPoint.recipeSpawnPoint.rotation);
            recipeText.mixingIngredientsTask = mixingIngredientsTask;
            mixingIngredientsTask.AddLinkedGameObject(recipeText.gameObject);

            
            // spawn ingredient containers
            int spawnPointIndex = 0;
            foreach (var ingredientType in MixingIngredients.IngredientTypes)
            {
                var position = spawnPoint.ingredientContainerSpawnPoints[spawnPointIndex].position;
                
                // spawn ingredient bottle, set type and register event handler to add the spawned objects to linked objects
                IngredientSpawner ingredientSpawner = Instantiate(ingredientSpawnerPrefab, position, Quaternion.identity);
                ingredientSpawner.ingredientType = ingredientType;
                ingredientSpawner.OnGameObjectSpawnedEvent += mixingIngredientsTask.AddLinkedGameObject;
                mixingIngredientsTask.AddLinkedGameObject(ingredientSpawner.gameObject);
                
                spawnPointIndex++;
            }
            
            // spawn watering container
            WateringLiquidSpawner wateringContainer = Instantiate(wateringLiquidSpawnerPrefab, spawnPoint.wateringBottleSpawnPoint.position, Quaternion.identity);
            wateringContainer.OnGameObjectSpawnedEvent += mixingIngredientsTask.AddLinkedGameObject;
            mixingIngredientsTask.wateringLiquidSpawner = wateringContainer;
            mixingIngredientsTask.AddLinkedGameObject(wateringContainer.gameObject);
            
            // spawn plant to water
            WateringDetection plantToWater = Instantiate(plantToWaterPrefab, spawnPoint.plantToWaterSpawnPoint.position, Quaternion.identity);
            plantToWater.OnWateringDetected += mixingIngredientsTask.OnWateringDetected;
            mixingIngredientsTask.AddLinkedGameObject(plantToWater.gameObject);
            
            return mixingIngredientsTask;
        }
    }
}