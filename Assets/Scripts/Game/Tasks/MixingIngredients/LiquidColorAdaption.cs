using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Enables to change the color of the liquid using the <see cref="liquidMeshRenderer"/>
    /// </summary>
    public class LiquidColorAdaption : MonoBehaviour
    {
        [SerializeField] private MeshRenderer liquidMeshRenderer;

        /// <summary>
        /// Changes liquid "Color" attribute to given color
        /// </summary>
        /// <param name="color">new liquid color</param>
        public void UpdateColor(Color color)
        {
            liquidMeshRenderer.material.color = color;
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
