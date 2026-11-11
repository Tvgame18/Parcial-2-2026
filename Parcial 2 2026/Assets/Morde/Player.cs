using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionReference inputMove;
    public InputActionReference inputJump;
    public Rigidbody rb;
    public float speed =5f;
    public float jumpForce = 5f;
    public bool isGrounded;
    public Vector2 movement;
    public Animator myAnimator;
    public float coyoteTime;
    public float coyoteTimeLimit = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = inputMove.action.ReadValue<Vector2>();
       
        myAnimator.SetBool("Run", movement != Vector2.zero);
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (coyoteTime <= coyoteTimeLimit)
        {
            coyoteTime += Time.deltaTime;
        }

        if (isGrounded)
        {
            coyoteTime = coyoteTimeLimit;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        if (inputJump.action.WasPerformedThisFrame() && coyoteTime > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            myAnimator.SetBool("Jump", true);
            isGrounded = false;
            coyoteTime = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, 0);
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") &&
        rb.linearVelocity.y <= 0.1f)
        {
            myAnimator.SetBool("Jump", false);
            isGrounded = true;
        }
        
           
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

