using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 7.0f; // The distance at which the enemy will stop following the player
    public float shotInterval = 0.5f; // The time interval between shots
    public float enemySpeed = 5;
    public float shootSpeed = 10f;
    public float platformEdgeCheckDistance = 2f;
    public float scanRadius = 20;
    public int platformAngle = 20;
    public GameObject arrowPrefab; // The arrow prefab to be shot
    public Transform ShootPos;

    private Transform player; // Reference to the player's transform
    private float timeSinceLastShot = 0.0f; // The time elapsed since the last shot was fired
    private int platformRightAngle;
    private int platformLeftAngle;
    private bool isFollowingPlayer = false;
    private Vector2 angleVectorRight;
    private Vector2 angleVectorLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform; // Get the player's transform

        platformRightAngle = 180 - platformAngle;
        float radians = platformRightAngle * Mathf.Deg2Rad;
        angleVectorRight = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        platformLeftAngle = 180 + platformAngle;
        radians = platformRightAngle * Mathf.Deg2Rad;
        angleVectorLeft = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    // Update is called once per frame
    void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = gameObject.transform.position.x - player.position.x;
        float differenceToPlayer = Mathf.Abs(distanceToPlayer);

        // If the player is farther away than the follow distance, follow them
        if (differenceToPlayer < scanRadius)
        {
            if (differenceToPlayer > stoppingDistance)
            {
                if (!(distanceToPlayer < 0 && IsAtEdgeOfPlatform(angleVectorRight)) && !(distanceToPlayer > 0 && IsAtEdgeOfPlatform(angleVectorLeft)))
                    transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);

                isFollowingPlayer = true;
            }
            else
            {
                isFollowingPlayer = false;
            }

            // Increment the time since last shot
            timeSinceLastShot += Time.deltaTime;

            // If the shot interval has passed, fire an arrow
            if (timeSinceLastShot >= shotInterval)
            {
                // Reset the time since last shot
                timeSinceLastShot = 0.0f;

                // Fire an arrow in the direction of the player
                Shoot();
            }
        }
        else
        {
            isFollowingPlayer = false;
        }



        if (isFollowingPlayer)
        {
            if (distanceToPlayer < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (distanceToPlayer > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, ShootPos.position, Quaternion.identity);
        Vector2 direction = (player.position - ShootPos.position).normalized;
        arrow.GetComponent<Rigidbody2D>().velocity = direction * shootSpeed;

        Vector3 lookDirection = arrow.GetComponent<Rigidbody2D>().velocity.normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private bool IsAtEdgeOfPlatform(Vector2 checkVector)
    {
        RaycastHit2D hit = Physics2D.Raycast(ShootPos.position, Vector2.down, platformEdgeCheckDistance);
        return !(hit.collider != null && hit.collider.CompareTag("Ground"));
    }
}
