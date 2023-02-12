using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpTime;
    public float checkRadius;
    public float j = 0.1f;
    public LayerMask whatIsGround;
    public Transform BottomPos;

    private Rigidbody2D rb;
    private float moveInput;
    private float jumpTimeCounter;
    private float k;
    private bool isJumping = false;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(BottomPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            isJumping = true;
            jumpTimeCounter = jumpTime;
            k = j;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * (jumpForce - k);
                jumpTimeCounter -= Time.deltaTime;
                k += j;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    
}