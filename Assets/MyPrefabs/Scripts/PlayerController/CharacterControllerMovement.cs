using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 7f;
    public float sprintIncrease = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float currentSpeed = speed;

        // modify current speed if sprint button pressed
        if (Input.GetKey(sprintKey))
        {
            currentSpeed += sprintIncrease;
        }
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (currentSpeed * Time.deltaTime));

        if (Input.GetKey(jumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
