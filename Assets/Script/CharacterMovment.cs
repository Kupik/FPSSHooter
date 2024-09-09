using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f; // Viteza de sprint
    public float jumpForce = 8f;
    public float gravity = 20f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float ySpeed;

    public AudioSource walk;
    public AudioSource shiftRun;
    public AudioSource jump;

    private bool isWalking = false;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Mișcarea pe orizontală
        float horizontal = Input.GetAxis("Horizontal");
        // Mișcarea pe verticală
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isWalking = move.magnitude > 0;

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = move * currentSpeed;

            // Săritura
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
                if (jump)
                {
                    jump.Play();
                }
            }
            else
            {
                ySpeed = -0.1f;
            }

            if (isWalking)
            {
                if (isSprinting)
                {
                    if (!shiftRun.isPlaying) 
                    {
                        shiftRun.Play();
                    }
                }
                else
                {
                    if (!walk.isPlaying) 
                    {
                        walk.Play();
                    }
                }
            }
            else
            {
                if (walk.isPlaying)
                {
                    walk.Stop();
                }
                if (shiftRun.isPlaying)
                {
                    shiftRun.Stop();
                }
            }
        }
        else
        {
            ySpeed -= gravity * Time.deltaTime;
        }

        moveDirection.y = ySpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
