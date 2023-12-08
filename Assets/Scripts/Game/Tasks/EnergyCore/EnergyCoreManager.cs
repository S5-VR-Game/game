using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreManager : MonoBehaviour
    {
        private StartEnergyCoreTask _startEnergyCoreTaskScript;
        
        public GameObject[] energyCores; // stores the objects of the filled cores and the empty one (last index)
        public GameObject[] energyCells; // stores the objects of the cells to put in the cores

        private readonly List<GameObject> _emptyCores = new(); // stores the reference to the empty cells (max two possible)
        
        private bool _energyCellInCore = true; // value to set: true, if picked up energy cell is back in a core
                                               //               false, if the picked up energy cell is still grabbed
                                               
        private void Start()
        {
            _startEnergyCoreTaskScript = GetComponent<StartEnergyCoreTask>(); 
            
            _emptyCores.Add(energyCores[^1]); // adds the reference of the empty core to the array 
        }

        // function disables grabInteractable-script from all cells which are remaining in cores
        public void OnEnergyCellSelected()
        {
            if (!_energyCellInCore) return;
            
            _energyCellInCore = false;

            foreach (var energyCell in energyCells)
            {
                if (energyCell != null)
                {
                    if (energyCell.GetComponent<XRGrabInteractable>().isSelected)
                        energyCell.GetComponent<XRGrabInteractable>().enabled = false;
                }
            }
        }

        // checks if collision between cell and core happened
        public void TriggerOnCollisionEnter(GameObject core, GameObject cell)
        {
            if (!_emptyCores.Contains(core)) return; // checks if core is empty

            _energyCellInCore = true; // allows other cells to get picked up

            if (!ManageEnergyCoreColors.GetColor(core)
                    .Equals(ManageEnergyCoreColors.GetColor(cell)))
                return; // checks if core and cell have the same color

            _emptyCores.Remove(core); // core is not empty anymore, removed from list

            _startEnergyCoreTaskScript.finishedEnergyCoreCounter++; // adds one to the counter: 7 = win

            Destroy(core); // destroys the core
            Destroy(cell); // and the cell if both have the same color

            //activates all grabInteractable-scripts
            foreach (var energyCell in energyCells)
            {
                if (energyCell != null)
                {
                    energyCell.GetComponent<XRGrabInteractable>().enabled = true;
                }
            }
        }

        // checks if the cell is picked up from the core
        public void TriggerOnCollisionExit(GameObject core)
        {
            Debug.Log("Added Core " + core + " to empty cores.");
            _emptyCores.Add(core);
        }
    }
}