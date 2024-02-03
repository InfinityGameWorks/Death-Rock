using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Transform orientation;
    public float speed = 5f; // Kecepatan gerakan karakter
    public float sensitivity = 2f; // Kecepatan gerakan karakter
    public float jumpForce = 10f; // Kecepatan lompat karakter
    private bool isGrounded; // Untuk mengecek apakah karakter menyentuh tanah
    private Vector2 moveInput;
    Vector3 moveDirection;
    private Rigidbody rb;

    public LayerMask ground;
    public float groundDrag;
    public float playerHeight;
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        
        // Lompat
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        MoveForward();
        SpeedControl();
    }

    public void MoveInput(InputAction.CallbackContext context){
        if (context.performed)
        {
            if (isGrounded)
            {

            }
                moveInput = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            moveInput = Vector2.zero;
        }
    }
   

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void MoveForward()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        //Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * speed * Time.deltaTime;

        // Menggerakkan karakter
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    

    /*

     */


    void OnCollisionEnter(Collision collision)
    {
        // Cek jika karakter menyentuh tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
