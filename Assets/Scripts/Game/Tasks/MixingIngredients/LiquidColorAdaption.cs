using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Enables to change the color of the liquid using the <see cref="liquidMeshRenderer"/>
    /// </summary>
    public class LiquidColorAdaption : MonoBehaviour
    {
        [SerializeField] private MeshRenderer liquidMeshRenderer;
        private static readonly int LiquidColor1Attribute = Shader.PropertyToID("_Color1");

        /// <summary>
        /// Changes liquid "Color1" attribute to given color
        /// </summary>
        /// <param name="color">new liquid color</param>
        public void UpdateColor(Color color)
        {
            liquidMeshRenderer.material.SetColor(LiquidColor1Attribute, color);
        }
        
        /// <summary>
        /// Activates or deactivates the game object related to the liquid.
        /// This will hide/show the liquid.
        /// </summary>
        /// <param name="active">determines the active state of the liquid</param>
        public void SetActive(bool active)
        {
            liquidMeshRenderer.gameObject.SetActive(active);
        }
    }
}
