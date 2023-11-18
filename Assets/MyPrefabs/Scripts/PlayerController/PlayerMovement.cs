using UnityEngine;

namespace MyPrefabs.Scripts.PlayerController
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;

        public float groundDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        private bool m_ReadyToJump;

        [Header("Keybinds")] 
        public KeyCode jumpKey = KeyCode.Space;
    
        [Header("Ground Check")] 
        public float playerHeight;
        public LayerMask whatIsGround;
        private bool m_Grounded;
    
        public Transform orientation;

        private float m_HorizontalInput;
        private float m_VerticalInput;

        private Vector3 m_MoveDirection;

        private Rigidbody m_Rb;

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
            m_Rb.freezeRotation = true;
            m_ReadyToJump = true;
        }

        void Update()
        {
            // check if player is grounded using a ray cast downwards 
            m_Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
            UpdateInput();
            SpeedControl();

            if (m_Grounded)
                m_Rb.drag = groundDrag;
            else
                m_Rb.drag = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void UpdateInput()
        {
            m_HorizontalInput = Input.GetAxisRaw("Horizontal");
            m_VerticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(jumpKey) && m_ReadyToJump && m_Grounded)
            {
                m_ReadyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer()
        {
            m_MoveDirection = orientation.forward * m_VerticalInput + orientation.right * m_HorizontalInput;

            if (m_Grounded)
                m_Rb.AddForce(m_MoveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            else if(!m_Grounded)
                m_Rb.AddForce(m_MoveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            
            
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(m_Rb.velocity.x, 0f, m_Rb.velocity.z);
        
            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                m_Rb.velocity = new Vector3(limitedVel.x, m_Rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            m_Rb.velocity = new Vector3(m_Rb.velocity.x, 0f, m_Rb.velocity.z);
        
            m_Rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            m_ReadyToJump = true;
        }
    }
}
