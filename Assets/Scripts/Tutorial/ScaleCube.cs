using PlayerController;
using UnityEngine;

namespace Tutorial
{
    public class ScaleObject : MonoBehaviour
    {
        private const float ScalingSpeed = 30.0f;
        
        // The scale factor you want to apply
        public Vector3 scaleFactor = new Vector3(1f, 1f, 1f) * ScalingSpeed * Time.deltaTime;

        // Minimum and maximum scale values
        public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 maxScale = new Vector3(5f, 5f, 5f);
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
                ScaleCube(scaleFactor);
            }

            // Scale down when the "ScaleDown" button is pressed
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ScaleCube(-scaleFactor);
            }
        }

        private void ScaleCube(Vector3 additionalScale)
        {
            // Access the Transform component of the game object
            var objectTransform = transform;

            // Calculate the new scale by adding the additional scale
            var newScale = objectTransform.localScale + additionalScale;

            // Clamp the scale values to stay within the specified min and max values
            newScale = Vector3.Max(minScale, newScale);
            newScale = Vector3.Min(maxScale, newScale);

            // Modify the localScale property to scale the object
            objectTransform.localScale = newScale;
        }
    }
}