using UnityEngine;

public class Ghost : MonoBehaviour
{
    GameObject PossiblePlayer;

    [SerializeField] private float moveSpeed = 5f;
    private bool isInsideEnemy = false;

    private void Awake()
    {
        FindObjectOfType<GameManager>().isGhostInScene = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveInput = new Vector3(horizontalInput, verticalInput).normalized;

        transform.position += moveInput * moveSpeed * Time.deltaTime;
        if(isInsideEnemy && Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<GameManager>().isGhostInScene = false;
            PatrolEnemy patrolEnemy = PossiblePlayer.GetComponent<PatrolEnemy>();
            StaticEnemy staticEnemy = PossiblePlayer.GetComponent<StaticEnemy>();

            if (patrolEnemy != null)
            {
                patrolEnemy.enabled = false;
            }

            if (staticEnemy != null)
            {
                staticEnemy.enabled = false;
            }

            PossiblePlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            PossiblePlayer.AddComponent<PlayerController>();
            PossiblePlayer.AddComponent<Shooting>();
            PossiblePlayer.tag = "Player";
            if (PossiblePlayer.GetComponent<Instances>().isPlayerFacingRight)
                PossiblePlayer.transform.Rotate(0f, 180f, 0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            isInsideEnemy = true;
            PossiblePlayer = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isInsideEnemy = false;
        }
    }
}
