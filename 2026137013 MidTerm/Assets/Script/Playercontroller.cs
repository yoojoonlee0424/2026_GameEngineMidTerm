using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Playercontroller : MonoBehaviour
{

    public float MoveSpeed = 1.0f;
    public float JumpForce = 1.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;

    private bool isGiant = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
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


        if(isGiant)
        {
            if (moveInput < 0)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            else if (moveInput > 0)
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
        }
        else
        {
            if (moveInput < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveInput > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }


        

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
            pAni.SetTrigger("Jump");
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

        }

        if(collision.CompareTag("Finish"))
        {
           collision.GetComponent<LevelObject>().LoadNextLevel();

        }

        if(collision.CompareTag("Enemy"))
        {
            if(isGiant)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }

        if (collision.CompareTag("item"))
        {
            isGiant = true;
            Destroy(collision.gameObject);
        }
    }





}
