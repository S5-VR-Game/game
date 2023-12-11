using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Mixing ingredients task spawn point. Provides spawn points for the recipe, the ingredient containers, the plant
    /// and the watering bottle.
    /// </summary>
    public class MixingIngredientsSpawnPoint : TaskSpawnPoint
    {
        public Transform recipeSpawnPoint;
        
        public Transform[] ingredientContainerSpawnPoints;
        
        public Transform plantToWaterSpawnPoint;
        
        public Transform wateringBottleSpawnPoint;
    }
}