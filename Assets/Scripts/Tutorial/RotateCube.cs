using PlayerController;
using UnityEngine;

namespace Tutorial
{
    public class RotateCube : MonoBehaviour
    {
        private const float ControlDistance = 4.0f;
        private const float RotationSpeed = 50.0f;
        [SerializeField]private PlayerProfileService playerProfileService;

        private void Update()
        {
            if (Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) > ControlDistance)
            {
                return;
            }
            if (Input.GetKey(KeyCode.JoystickButton0))
            {
                transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.JoystickButton1))
            {
                transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
            }
        }
    }
}