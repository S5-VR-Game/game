using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Represents an ingredient in the mixing ingredients task. Allows to change the color of the liquid according to
    /// the ingredient type. 
    /// </summary>
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private LiquidColorAdaption liquidColorAdaption;
        private IngredientType m_Type;

        /// <summary>
        /// Sets the ingredient type and updates the color of the liquid
        /// </summary>
        /// <param name="ingredientType">new ingredient type</param>
        public void SetIngredientType(IngredientType ingredientType)
        {
            m_Type = ingredientType;
            liquidColorAdaption.UpdateColor(m_Type.color);
        }
        
        /// <summary>
        /// Provides the ingredient type
        /// </summary>
        /// <returns>current ingredient type</returns>
        public IngredientType GetIngredientType()
        {
            return m_Type;
        }
    }
}
