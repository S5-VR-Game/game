using System;
using TMPro;
using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Updates the recipe text mesh with the recipe text from the mixing ingredients task
    /// </summary>
    public class RecipeText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;
        [NonSerialized] public MixingIngredients mixingIngredientsTask;

        private void Start()
        {
            // build recipe text from the recipe dictionary and update the text mesh
            textMesh.text = mixingIngredientsTask.GetRecipeText();
        }
    }
}
