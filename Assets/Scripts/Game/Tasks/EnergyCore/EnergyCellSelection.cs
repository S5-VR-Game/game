using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCellSelection : MonoBehaviour
    {
        public EnergyCoreManager energyCoreManager;

        public void OnEnergyCellSelection()
        {
            energyCoreManager.OnEnergyCellSelected();
        }
    }
}