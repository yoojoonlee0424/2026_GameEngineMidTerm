using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{

    public float MoveSpeed = 1.0f;
    public float JumpForce = 1.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }




    public void OnMove(InputValue Value)
    {
        Vector2 input = Value.Get<Vector2>();
        moveInput = input.x;
    }
    

    public void OnJump(InputValue Value)
    {
        if(Value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }






}
