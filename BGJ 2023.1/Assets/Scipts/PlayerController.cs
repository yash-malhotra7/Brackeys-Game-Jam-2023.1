using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Instances inst;

    private GameObject GhostPrefab;

    private float moveSpeed = 5f;
    private float jumpForce = 10f;
    private float jumpTime;
    private float checkRadius;
    private float jumpVelocityReductionRate = 0.1f;
    private float ghostSpawnTime = 1f;
    public bool isPlayerFacingRight = true;
    private LayerMask whatIsGround;
    private Transform BottomPos;
    private Rigidbody2D rb;
    private float moveInput;
    private float jumpTimeCounter;
    private float k;
    private bool isJumping = false;
    private bool isGrounded;

    private void Awake()
    {
        inst = GetComponent<Instances>();
    }

    void Start()
    {
        GhostPrefab = inst.GhostPrefab;

        moveSpeed = 5f;
        jumpForce = 10f;
        jumpTime = inst.jumpTime;
        checkRadius = inst.checkRadius;
        jumpVelocityReductionRate = inst.jumpVelocityReductionRate;
        ghostSpawnTime = inst.ghostSpawnTime;
        whatIsGround = inst.whatIsGround;
        BottomPos = inst.BottomPos;

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
            isPlayerFacingRight = true;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isPlayerFacingRight = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            isJumping = true;
            jumpTimeCounter = jumpTime;
            k = jumpVelocityReductionRate;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * (jumpForce - k);
                jumpTimeCounter -= Time.deltaTime;
                k += jumpVelocityReductionRate;
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

    public void SpawnTheGhost()
    {
        Invoke("SpawnGhost", ghostSpawnTime);
    }

    void SpawnGhost()
    {
        Instantiate(GhostPrefab, transform.position, Quaternion.identity);
    }
}