using UnityEngine;

namespace PlayerController
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
    
        public Transform playerBody;

        private float m_XRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            m_XRotation -= mouseY;
            // clamp to prevent from looking higher or lower than 90 degree
            m_XRotation = Mathf.Clamp(m_XRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(m_XRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
