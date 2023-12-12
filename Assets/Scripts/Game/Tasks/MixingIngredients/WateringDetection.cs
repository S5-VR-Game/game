using System;
using UnityEngine;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Detects watering liquid entering the trigger collider and notifies the listeners
    /// </summary>
    public class WateringDetection : MonoBehaviour
    {
        public event Action OnWateringDetected;

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.gameObject.GetComponentInParent<WateringLiquid>();
            if (ingredient != null)
            {
                OnWateringDetected?.Invoke();
            }
        }
    }
}
