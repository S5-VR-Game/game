using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Main entry point for the mixing ingredients task. Provides constants, handles basic task logic and control flow.
    /// </summary>
    public class MixingIngredients : GameTask
    {
        private const string Name = "Mixing ingredients";
        private const string Description = "Task decsription";
        private const string RecipeHeaderText = "Recipe";
        
        /// <summary>
        /// Line format for the recipe text. First parameter is the quantity, second parameter is the ingredient name.
        /// </summary>
        private const string RecipeLineFormat = "{0:D}x {1}";

        /// <summary>
        /// Determines the maximum number of ingredients needed for the recipe and the task to complete.
        /// The maximum number will be used only in maximum difficulty scenario.
        /// Must be smaller or equal to the number of ingredient types stored in <see cref="IngredientTypes"/>.
        /// </summary>
        private const int MaxIngredients = 4;

        /// <summary>
        /// Minimum possible quantity that is necessary to complete the recipe.
        /// Actual value is influenced by the current difficulty.
        /// </summary>
        private const int MinIngredientQuantity = 1;

        /// <summary>
        /// Maximum possible quantity that is necessary to complete the recipe. 
        /// Actual value is influenced by the current difficulty.
        /// </summary>
        private const int MaxIngredientQuantity = 5;

        private const float WateringBottleFillDistance = 0.5f;

        private Color m_WateringLiquidColor;

        /// <summary>
        /// All available ingredient types. A specific number of ingredients will be randomly selected from this array
        /// to create the recipe.
        /// </summary>
        public static readonly IngredientType[] IngredientTypes =
        {
            IngredientType.Green, IngredientType.Blue, IngredientType.Yellow, IngredientType.Cyan,
            IngredientType.Magenta
        };

        [SerializeField] private IngredientsDetection ingredientsDetection;
        [SerializeField] private Transform wateringBottleNearbyCheck;
        [SerializeField] private LiquidColorAdaption liquidColorAdaption;
        [NonSerialized] public WateringLiquidSpawner wateringLiquidSpawner;
        
        /// <summary>
        /// Holds the valid ingredients and their quantity that are necessary to complete the recipe.
        /// </summary>
        private readonly Dictionary<IngredientType, int> m_ValidIngredients = new();
        private bool m_WrongIngredientDetected;
        private bool m_AllIngredientsDetected;
        private bool m_WateringBottleFilledUp;
        private bool m_TaskCompleted;

        public MixingIngredients() : base(Name, Description)
        {
            taskDescription = "Du brauchst nur etwas mehr Sauerstoff um zu überleben.\n" +
                              "Du musst die richtigen Zutaten in dem Hauptbehälter mischen, um einen Superdünger zu erstellen.\n" +
                              "Fülle die Vase mit den richtigen Zutaten.\n" +
                              "Wenn du fertig bist, fülle die leere Flasche mit dem Dünger und gieße die Pflanze.\n" +
                              "Der Botaniker sagte, sein Rezept sollte hier irgendwo sein.\n";
        }

        public override void Initialize()
        {
            if (MaxIngredients > IngredientTypes.Length)
            {
                // MaxIngredients must not be greater than the length of available ingredients
                throw new ArgumentOutOfRangeException();
            }
            
            // initialize watering color with random color
            m_WateringLiquidColor = Random.ColorHSV();

            DetermineValidIngredients();
            
            // register event handlers
            ingredientsDetection.OnIngredientDetected += OnIngredientDetected;
        }

        /// <summary>
        /// Handles the event when an ingredient is thrown into the mixing container
        /// </summary>
        /// <param name="ingredient"></param>
        private void OnIngredientDetected(IngredientType ingredient)
        {
            // check if ingredient is valid
            if (m_ValidIngredients.ContainsKey(ingredient))
            {
                // decrement ingredient quantity to keep track of the remaining ingredients
                m_ValidIngredients[ingredient]--;
                
                // remove ingredient from dictionary if the necessary quantity is fulfilled
                if (m_ValidIngredients[ingredient] <= 0)
                {
                    m_ValidIngredients.Remove(ingredient);
                }
                
                // check if all ingredients are detected
                if (m_ValidIngredients.Count == 0)
                {
                    // colorize liquid of main container
                    m_AllIngredientsDetected = true;
                    liquidColorAdaption.SetActive(true);
                    liquidColorAdaption.UpdateColor(m_WateringLiquidColor);
                }
            }
            else
            {
                // wrong ingredient detected
                m_WrongIngredientDetected = true;
            }
        }

        /// <summary>
        /// Handles the event when the watering bottle is tried to fill up from the mixing pot
        /// </summary>
        private void OnWateringSpawnerFillTry()
        {
            // only allow filling up, if all ingredients are detected
            if (!m_WateringBottleFilledUp && m_AllIngredientsDetected)
            {
                m_WateringBottleFilledUp = true;
                wateringLiquidSpawner.FillLiquid(m_WateringLiquidColor);
            }
        }

        /// <summary>
        /// Handles the event when the plant is watered
        /// </summary>
        public void OnWateringDetected()
        {
            m_TaskCompleted = true;
        }

        /// <summary>
        /// Determines the valid ingredients and sets up the <see cref="m_ValidIngredients"/> dictionary with valid
        /// ingredient types and their quantity
        /// </summary>
        private void DetermineValidIngredients()
        {
            float difficultyPercentage = difficulty.GetPercentage();
            // determine number of ingredients according to current difficulty, ensure a minimum value of 1
            int numberOfIngredients = 1 + (int)((MaxIngredients-1) * difficultyPercentage);
            // shuffle ingredient types for random result
            IngredientTypes.Shuffle();

            // add first n ingredients to the valid ingredients dictionary, where n is the number of ingredients
            // and assign them a random quantity according to the current difficulty and the min/max ingredient quantity
            for (int index = 0; index < numberOfIngredients; index++)
            {
                var randomQuantity = Random.Range(MinIngredientQuantity, MaxIngredientQuantity);
                m_ValidIngredients.Add(IngredientTypes[index], 1 + (int)(randomQuantity * difficultyPercentage));
            }
        }

        /// <summary>
        /// Creates a formatted recipe string from the <see cref="m_ValidIngredients"/> dictionary
        /// </summary>
        /// <returns>formatted recipe text</returns>
        public string GetRecipeText()
        {
            StringBuilder recipeText = new StringBuilder();
            recipeText.AppendLine(RecipeHeaderText);
            foreach (var entry in m_ValidIngredients)
            {
                recipeText.AppendLine(string.Format(RecipeLineFormat, entry.Value, entry.Key.name));
            }
            
            return recipeText.ToString();
        }

        protected override void BeforeStateCheck()
        {
            // check if watering bottle is nearby with distance check, because collision detection is not working when
            // grab interactable object is grabbed in vr
            if (Vector3.Distance(wateringBottleNearbyCheck.position, wateringLiquidSpawner.GetBottlePosition()) <=
                WateringBottleFillDistance)
            {
                // watering bottle is nearby, allow filling up
                OnWateringSpawnerFillTry();
            }
        }

        protected override TaskState CheckTaskState()
        {
            if (m_TaskCompleted)
            {
                return TaskState.Successful;
            }
            
            if (m_WrongIngredientDetected)
            {
                return TaskState.Failed;
            }

            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}