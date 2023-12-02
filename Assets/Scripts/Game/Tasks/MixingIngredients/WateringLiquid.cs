using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Updates the watering liquid color to the static <see cref="MixingIngredients.WateringLiquidColor"/> color
    /// </summary>
    public class WateringLiquid : MonoBehaviour
    {
        [SerializeField] private LiquidColorAdaption liquidColorAdaption;

        private void Start()
        {
            liquidColorAdaption.UpdateColor(MixingIngredients.WateringLiquidColor);
        }
    }
}
