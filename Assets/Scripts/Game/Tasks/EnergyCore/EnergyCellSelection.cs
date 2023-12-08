using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCellSelection : MonoBehaviour
    {
        public EnergyCoreManager energyCoreManager;

        // event triggered when a energy cell is picked up by the player
        public void OnEnergyCellSelection()
        {
            Debug.Log("Picked up Energy Cell");
            energyCoreManager.OnEnergyCellSelected();
        }
    }
}