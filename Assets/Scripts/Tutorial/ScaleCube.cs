using PlayerController;
using UnityEngine;

namespace Tutorial
{
    public class ScaleObject : MonoBehaviour
    {
        private const float ScalingSpeed = 50.0f;

        // Minimum and maximum scale values
        private const float ControlDistance = 4.0f;
        [SerializeField]private PlayerProfileService playerProfileService;


        void Update()
        {
            if (Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) > ControlDistance)
            {
                return;
            }
            // Scale up when the "ScaleUp" button is pressed
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ScaleCube(ScalingSpeed);
            }

            // Scale down when the "ScaleDown" button is pressed
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ScaleCube(-ScalingSpeed);
            }
        }

        private void ScaleCube(float scale)
        {
            transform.localScale *= scale * Time.deltaTime;
        }
    }
}