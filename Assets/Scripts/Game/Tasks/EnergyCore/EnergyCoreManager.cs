using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreManager : MonoBehaviour
    {
        public GameObject[] energyCores; // stores the objects of the filled cores and the empty one (last index)
        public GameObject[] energyCells; // stores the objects of the cells to put in the cores

        private List<GameObject> _emptyCores = new(); // stores the reference to the empty cells (max two possible)
        
        private XRGrabInteractable[] _cellGrabInteractables; // stores the reference to the XRGrabInteractable script of 
                                                             // of each energy cell
        
        private bool _energyCellInCore = true; // value to set: true, if picked up energy cell is back in a core
                                               //               false, if the picked up energy cell is still grabbed
        
        private void Start()
        {
            _cellGrabInteractables = new XRGrabInteractable[energyCells.Length]; 
            
            for (var i = 0; i < energyCells.Length; i++)
            {
                // adds each reference of the grab-script to the array and adds listener if the cell is picked up
                _cellGrabInteractables[i] = energyCells[i].GetComponent<XRGrabInteractable>();
                _cellGrabInteractables[i].selectEntered.AddListener(OnEnergyCellSelected);
            }
            
            _emptyCores.Add(energyCores[^1]); // adds the reference of the empty core to the array 
        }

        // function disables grabInteractable-script from all cells which are remaining in cores
        private void OnEnergyCellSelected(SelectEnterEventArgs args)
        {
            if (!_energyCellInCore) return;
            
            _energyCellInCore = false; 
            
            foreach (var grabInteractable in _cellGrabInteractables)
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
            foreach (var core in energyCores)
            {
                if (collision.collider.gameObject == core) // if current core is the one which triggered the event
                {
                    foreach (var emptyCore in _emptyCores)
                    {
                        if (collision.collider.gameObject == emptyCore) // if current core has no cell
                        {
                            _emptyCores.Remove(emptyCore); // core is not empty anymore, removed from list
                            _energyCellInCore = true; // allows other cells to get picked up
                            
                            //activates all grabInteractable-scripts
                            foreach (var grabInteractable in _cellGrabInteractables)
                            {
                                grabInteractable.enabled = true;
                            }
                        }    
                    }
                }
            }
        }

        // checks if the cell is picked up from the core
        private void OnCollisionExit(Collision collision)
        {
            if (energyCores.Any(core => collision.collider.gameObject == core))
            {
                _emptyCores.Add(collision.collider.gameObject);
            }
        }
    }
}