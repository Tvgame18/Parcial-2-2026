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

        if (inputJump.action.WasPerformedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            myAnimator.SetBool("Jump",true);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, 0);
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            myAnimator.SetBool("Jump", false);
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

