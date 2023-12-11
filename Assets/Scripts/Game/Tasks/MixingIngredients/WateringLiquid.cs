using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Updates the watering liquid color to the static <see cref="MixingIngredients.wateringLiquidColor"/> color
    /// </summary>
    public class WateringLiquid : MonoBehaviour
    {
        [SerializeField] private LiquidColorAdaption liquidColorAdaption;
        
        /// <summary>
        /// Updates the color of this watering liquid
        /// </summary>
        /// <param name="color">new liquid color</param>
        public void UpdateColor(Color color)
        {
            liquidColorAdaption.UpdateColor(color);
        }

    }
}
