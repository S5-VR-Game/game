using System;
using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Detects the watering bottle entering the trigger collider and notifies the listeners
    /// </summary>
    public class WateringBottleDetection : MonoBehaviour
    {
        public event Action OnWateringSpawnerDetected;

        private void OnTriggerEnter(Collider other)
        {
            var wateringLiquidSpawner = other.gameObject.GetComponentInParent<WateringLiquidSpawner>();
            if (wateringLiquidSpawner != null)
            {
                OnWateringSpawnerDetected?.Invoke();
            }
        }
    }
}