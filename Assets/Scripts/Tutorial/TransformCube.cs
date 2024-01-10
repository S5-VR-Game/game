using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tutorial
{

    public class TransformCube : MonoBehaviour
    {
        private float moveAmount = 1f;
        private const float ControlDistance = 4.0f;
        [SerializeField]private PlayerProfileService playerProfileService;

        void Update()
        {
            if (Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) > ControlDistance)
            {
                return;
            }
            // Move the object up when the "MoveUp" button is pressed
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                MoveObject(Vector3.up * moveAmount * Time.deltaTime);
            }

            // Move the object down when the "MoveDown" button is pressed
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                MoveObject(Vector3.down * moveAmount * Time.deltaTime);
            }
        }

        void MoveObject(Vector3 moveVector)
        {
            // Access the Transform component of the game object
            var objectTransform = transform;

            // Modify the position of the object
            objectTransform.Translate(moveVector);
        }
    }
}