using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Ingredient types for the mixing ingredients task.
    /// </summary>
    public class IngredientType
    {
        public static readonly IngredientType Green = new IngredientType("Grün", Color.green);
        public static readonly IngredientType Blue = new IngredientType("Blau", Color.blue);
        public static readonly IngredientType Yellow = new IngredientType("Orange", Color.yellow);
        public static readonly IngredientType Cyan = new IngredientType("Türkis", Color.cyan);
        public static readonly IngredientType Magenta = new IngredientType("Magenta", Color.magenta);

        public string name { get; }
        public Color color { get; }

        private IngredientType(string name, Color color)
        {
            this.name = name;
            this.color = color;
        }
    }
}