using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Spawns watering liquid when rotated downwards and marked as filled.
    /// When filled, the color of the bottle is updated to the static <see cref="MixingIngredients.wateringLiquidColor"/>
    /// to indicate that the bottle is filled. Otherwise spawning is blocked and the liquid is deactivated. 
    /// </summary>
    public class WateringLiquidSpawner : RotateDownSpawner<WateringLiquid>
    {
        [SerializeField] private LiquidColorAdaption spawnerColorAdaption;
        [SerializeField] private Transform bottlePosition;

        private Color m_WateringLiquidColor;
        
        private void Start()
        {
            spawningEnabled = false;
        }

        public void FillLiquid(Color wateringLiquidColor)
        {
            m_WateringLiquidColor = wateringLiquidColor;
            spawningEnabled = true;
            spawnerColorAdaption.SetActive(true);
            spawnerColorAdaption.UpdateColor(m_WateringLiquidColor);
        }
        
        public Vector3 GetBottlePosition()
        {
            return bottlePosition.position;
        }

        protected override void OnSpawned(WateringLiquid newObject)
        {
            newObject.UpdateColor(m_WateringLiquidColor);
        }
    }
}