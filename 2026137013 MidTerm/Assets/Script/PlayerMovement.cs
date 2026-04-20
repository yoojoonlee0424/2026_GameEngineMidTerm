using System.Collections;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동&점프")]
    private float horizontal;
    public float MoveSpeed = 1.0f;
    public float JumpForce = 1.0f;
    public float JumpLow = 1.0f;
    private bool IsFacingRight = true;

    [Header("코요테 타임")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounte;

    [Header("대쉬")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1.0f;

    [Header("월점프 속도")]
    private bool isWallSliding = true;
    public float wallSlidingSpeed = 2.0f;

    [Header("월점프 지속시간")]
    private bool isWallJumping;
    private float WallJumpingDirection;
    private float wallJumpingtime = 0.2f;
    private float walljumpingCounter;
    public float walljumpingDuration = 0.4f;
    private Vector2 walljumpingPower = new Vector2(2f, 4f);


    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public TrailRenderer tr;
    public Animator anime;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }





    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");

        anime.SetBool("moving", horizontal != 0);

        if (isDashing)
        {
            return;
        }

        DashInput();


        coyote();

        jump();


        wallSlide();
        WallJump();

        

        if(!isWallJumping)
        {
            Flip();
        }

        

        


    }

    private void FixedUpdate()
    {

        if(isDashing)
        { 
            return; 
        }


        if(!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * MoveSpeed, rb.linearVelocity.y);
        }

        
    }


    private void coyote()
    {
        if (IsGrounded())
        {
            coyoteTimeCounte = coyoteTime;
        }
        else
        {
            coyoteTimeCounte -= Time.deltaTime;
        }
    }


    private void jump()
    {
        if (Input.GetButtonDown("Jump") && coyoteTimeCounte > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            //anime.SetTrigger("jump");
        }

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * JumpLow);

            coyoteTimeCounte = 0f;
        }

    }


    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }


    private void wallSlide()
    {
        if(isWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }


    private bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            WallJumpingDirection = -transform.localScale.x;
            walljumpingCounter = wallJumpingtime;


            CancelInvoke(nameof(StopWalljumping));
        }
        else
        {
            walljumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && walljumpingCounter > 0f)
        {
            isWallJumping =true;
            rb.linearVelocity = new Vector2(WallJumpingDirection * walljumpingPower.x, walljumpingPower.y);
            walljumpingCounter = 0f;
            
            if(transform.localScale.x != WallJumpingDirection)
            {
                IsFacingRight = !IsFacingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }

            Invoke(nameof(StopWalljumping), walljumpingDuration);
        }

    }



    private void StopWalljumping()
    {
        isWallJumping=false;
    }



    private void Flip()
    {
        if(IsFacingRight && horizontal < 0f || !IsFacingRight && horizontal > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }



    
        
    private void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originaGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        anime.enabled = false;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originaGravity;
        isDashing=false;

        anime.enabled = true;

        yield return new WaitForSeconds(dashingCooldown);

        canDash=true;


        
    }




}
