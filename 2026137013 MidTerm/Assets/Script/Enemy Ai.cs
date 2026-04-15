using UnityEngine;

public class EnemyAi : MonoBehaviour
{

    public float MoveSpeed = 1.0f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFacingRight)
        {
            rb.linearVelocity = new Vector2(MoveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-MoveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary"))
        {
            isFacingRight = !isFacingRight;
        }
    }



}
