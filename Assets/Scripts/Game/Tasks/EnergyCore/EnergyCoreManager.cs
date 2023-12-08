using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreManager : MonoBehaviour
    {
        private StartEnergyCoreTask _startEnergyCoreTaskScript; // reference to the StartEnergyCore-script
        
        public List<GameObject> energyCores = new(); // stores the objects of the filled cores and the empty one (last index)
        public List<GameObject> energyCells = new(); // stores the objects of the cells to put in the cores
        
        private readonly List<Color> _finishedCoreColors = new(); // stores the color of the cells and the cores which are colliding with each other
        
        private void Start()
        {
            _startEnergyCoreTaskScript = GetComponent<StartEnergyCoreTask>(); 
        }

        // function disables grabInteractable-script and activates isKinematic from all cells 
        public void OnEnergyCellSelected()
        {
            foreach (var energyCell in energyCells)
            {
                if (energyCell == null) return;
                if (energyCell.GetComponent<XRGrabInteractable>().isSelected) continue;
                
                energyCell.GetComponent<XRGrabInteractable>().enabled = false;
                energyCell.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        // checks if collision between cell and core happened
        public void TriggerOnCollisionEnter(GameObject core, GameObject cell)
        {
            // activates all grabInteractable-scripts
            // and sets isKinematic to true
            foreach (var energyCell in energyCells.Where(energyCell => energyCell != null))
            {
                if (_finishedCoreColors.Contains(cell.GetComponent<Renderer>().material.color)) continue;
                energyCell.GetComponent<XRGrabInteractable>().enabled = true;
                energyCell.GetComponent<Rigidbody>().isKinematic = false;
            }
            
            // checks if core and cell have the same color
            if (!ManageEnergyCoreColors.GetColor(core)
                    .Equals(ManageEnergyCoreColors.GetColor(cell)))
                return; // checks if core and cell have the same color

            // checks if color of the cell is in list of matching cores with cells
            if (_finishedCoreColors.Contains(cell.GetComponent<Renderer>().material.color)) return;
            
            _finishedCoreColors.Add(cell.GetComponent<Renderer>().material.color);
            _startEnergyCoreTaskScript.finishedEnergyCoreCounter++; // adds one to the counter: 6 = win
            
            // removes the core and the cell from its list and deactivates the possibility to grab the cell out of the core
            energyCores.Remove(core);
            energyCells.Remove(cell);
            cell.GetComponent<XRGrabInteractable>().enabled = false;
            cell.GetComponent<Rigidbody>().isKinematic = true;
            core.GetComponent<MeshCollider>().convex = true;
        }
    }
}