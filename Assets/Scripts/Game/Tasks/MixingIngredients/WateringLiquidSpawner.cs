using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Spawns watering liquid when rotated downwards and marked as filled.
    /// When filled, the color of the bottle is updated to the static <see cref="MixingIngredients.WateringLiquidColor"/>
    /// to indicate that the bottle is filled. Otherwise spawning is blocked and the liquid is deactivated. 
    /// </summary>
    public class WateringLiquidSpawner : RotateDownSpawner<WateringLiquid>
    {
        [SerializeField] private LiquidColorAdaption spawnerColorAdaption;
        [SerializeField] private Transform bottlePosition;

        private void Start()
        {
            spawningEnabled = false;
        }

        public void FillLiquid()
        {
            spawningEnabled = true;
            spawnerColorAdaption.SetActive(true);
            spawnerColorAdaption.UpdateColor(MixingIngredients.WateringLiquidColor);
        }
        
        public Vector3 GetBottlePosition()
        {
            return bottlePosition.position;
        }
    }
}