using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreManager : MonoBehaviour
    {
        private StartEnergyCoreTask startEnergyCoreTaskScript;
        
        public GameObject[] energyCores; // stores the objects of the filled cores and the empty one (last index)
        public GameObject[] energyCells; // stores the objects of the cells to put in the cores

        private readonly List<GameObject> emptyCores = new(); // stores the reference to the empty cells (max two possible)
        
        private XRGrabInteractable[] cellGrabInteractables; // stores the reference to the XRGrabInteractable script of 
                                                             // of each energy cell
        
        private bool energyCellInCore = true; // value to set: true, if picked up energy cell is back in a core
                                               //               false, if the picked up energy cell is still grabbed
        
        private void Start()
        {
            startEnergyCoreTaskScript = GetComponent<StartEnergyCoreTask>();
            
            cellGrabInteractables = new XRGrabInteractable[energyCells.Length]; 
            
            for (var i = 0; i < energyCells.Length; i++)
            {
                // adds each reference of the grab-script to the array and adds listener if the cell is picked up
                cellGrabInteractables[i] = energyCells[i].GetComponent<XRGrabInteractable>();
            }
            
            emptyCores.Add(energyCores[^1]); // adds the reference of the empty core to the array 
        }

        // function disables grabInteractable-script from all cells which are remaining in cores
        public void OnEnergyCellSelected()
        {
            if (!energyCellInCore) return;
            
            energyCellInCore = false; 
            
            foreach (var grabInteractable in cellGrabInteractables)
            {
                // deactivates the grabInteractable-script of each cell (except from the picked up)
                if (!grabInteractable.isSelected)
                {
                    grabInteractable.enabled = false;
                }
            }
        }

        // checks if collision between cell and core happened
        private void OnCollisionEnter(Collision collision)
        {
            foreach (var emptyCore in emptyCores)
            {
                if (collision.collider.gameObject == emptyCore) // if current core has no cell
                { 
                    emptyCores.Remove(emptyCore); // core is not empty anymore, removed from list
                    energyCellInCore = true; // allows other cells to get picked up
                            
                    HandleCollision(emptyCore, collision);
                    
                    //activates all grabInteractable-scripts
                    foreach (var grabInteractable in cellGrabInteractables)
                    {
                        grabInteractable.enabled = true;
                    }
                }    
            }
        }

        // checks if the cell is picked up from the core
        private void OnCollisionExit(Collision collision)
        {
            if (energyCores.Any(core => collision.collider.gameObject == core))
            {
                emptyCores.Add(collision.collider.gameObject);
            }
        }

        private void HandleCollision(GameObject energyCore, Collision other)
        {
            if (!other.gameObject.CompareTag("EnergyCell")) return;
                      
            if (!ManageEnergyCoreColors.GetColor(other.gameObject)
                    .Equals(ManageEnergyCoreColors.GetColor(gameObject))) return;
            
            if (emptyCores.Contains(other.collider.gameObject))
            { 
                startEnergyCoreTaskScript.finishedEnergyCoreCounter++;
                Destroy(energyCore);
                Destroy(other.gameObject);
            }
        }
    }
}