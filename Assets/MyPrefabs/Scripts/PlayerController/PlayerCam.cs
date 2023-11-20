using UnityEngine;

namespace MyPrefabs.Scripts.PlayerController
{
    public class PlayerCam : MonoBehaviour
    {
        public float sensX;
        public float sensY;
    
        public Transform orientation;

        private float m_XRotation;
        private float m_YRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // get mouse
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * this.sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * this.sensY;

            m_YRotation += mouseX;
            m_XRotation -= mouseY;
        
            m_XRotation = Mathf.Clamp(m_XRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(m_XRotation, m_YRotation, 0);
            orientation.rotation = Quaternion.Euler(0, m_YRotation, 0);
        }
    }
}
