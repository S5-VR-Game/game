using System;
using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Detects ingredients entering the collider and notifies the listeners
    /// </summary>
    public class IngredientsDetection : MonoBehaviour
    {
        public event Action<IngredientType> OnIngredientDetected;

        private void OnTriggerEnter(Collider other)
        {
            // check for ingredient script in parent, as the collider is on the child
            var ingredient = other.gameObject.GetComponentInParent<Ingredient>();
            if (ingredient != null)
            {
                OnIngredientDetected?.Invoke(ingredient.GetIngredientType());
            }
        }
    }
}
