using UnityEngine;

namespace PlayerController
{
    /// <summary>
    /// Debug tool to toggle the active player using the T key
    /// </summary>
    public class ToggleActivePlayer : MonoBehaviour
    {
        public PlayerProfileService service;
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                service.SetIsVrPlayerActive(!service.GetIsVrPlayerActive());
            }
        }
    }
}