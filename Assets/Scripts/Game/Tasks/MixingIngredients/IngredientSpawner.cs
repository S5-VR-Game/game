using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Spawns ingredients of a given type when rotated downwards.
    /// Changes the color of the container according to the ingredient type
    /// </summary>
    public class IngredientSpawner : RotateDownSpawner<Ingredient>
    {
        /// <summary>
        /// Is used to change the color of the container
        /// </summary>
        [SerializeField] private LiquidColorAdaption spawnerColorAdaption;
        public IngredientType ingredientType;
        
        private void Start()
        {
            // set the color of the container to the color of the ingredient type
            spawnerColorAdaption.UpdateColor(ingredientType.color);
        }

        protected override void OnSpawned(Ingredient newObject)
        {
            newObject.SetIngredientType(ingredientType);
        }
    }
}
