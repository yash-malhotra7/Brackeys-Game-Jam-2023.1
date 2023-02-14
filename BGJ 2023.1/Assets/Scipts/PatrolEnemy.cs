using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public GameObject arrowPrefab; // The prefab for the arrow object
    CheckBox checkBox;
    StopBox stopBox;

    public Transform pointA; // The leftmost point of the patrol
    public Transform pointB; // The rightmost point of the patrol
    public Transform ShootPos;
    public float moveSpeed = 3f; // The speed at which the enemy moves
    public float detectionRange = 20f; // The range at which the enemy can detect the player
    public float stoppingDistance = 5f;
    public float arrowSpeed = 10f; // The speed at which the arrow moves
    public float shootInterval = 2f; // The time between shots

    private float shootTimer = 0f; // The time since the last shot was fired
    private bool isChasingPlayer = false;
    public bool isPlayerFacingRight = true;
    private Vector3 pointAPos;
    private Vector3 pointBPos;

    private void Awake()
    {
    }

    private void Start()
    {
        checkBox = GetComponentInChildren<CheckBox>();
        stopBox = GetComponentInChildren<StopBox>();
        pointAPos = pointA.position;
        pointBPos = pointB.position;
    }

    void Update()
    {
        Debug.Log(isChasingPlayer);
        if(FindObjectOfType<PlayerController>() == null)
        {
            checkBox.isPlayerDetected = false;
        }
        if (isChasingPlayer)
        {
            // If the player is out of range, stop chasing and resume patrolling
            if (!checkBox.isPlayerDetected)
            {
                isChasingPlayer = false;
            }
            else
            {
                ChasePlayer();
            }
        }
        // Otherwise, patrol between pointA and pointB
        else
        {
            if (transform.position.x < pointAPos.x || transform.position.x > pointBPos.x)
                GoToNearestPatrolPoint();
            else
                Patrol();

            // If the player is within range, start chasing the player
            if (checkBox.isPlayerDetected)
            {
                isChasingPlayer = true;
            }
        }
    }

    Vector3 GetClosestPatrolPoint(Vector3 position)
    {
        // Return the closest patrol point to the given position
        float distanceToA = Vector3.Distance(position, pointAPos);
        float distanceToB = Vector3.Distance(position, pointBPos);
        return (distanceToA < distanceToB) ? pointAPos : pointBPos;
    }

    void GoToNearestPatrolPoint()
    {
        Vector3 closestPatrolPoint = GetClosestPatrolPoint(transform.position).normalized;
        if(closestPatrolPoint.x > transform.position.x)
        {
            isPlayerFacingRight = true;
        }
        else
        {
            isPlayerFacingRight = false;
        }
        float moveDistance = moveSpeed * Time.deltaTime;
        transform.position += closestPatrolPoint * moveDistance;

        FaceTowardsMovement(closestPatrolPoint);
    }

    void Patrol()
    {
        // Move the enemy back and forth between the two patrol points
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (transform.position.x > pointBPos.x)
        {
            transform.position = new Vector2(pointBPos.x, transform.position.y);
            transform.Rotate(0f, 180f, 0f);
            isPlayerFacingRight = false;
        }
        else if (transform.position.x < pointAPos.x)
        {
            transform.position = new Vector2(pointAPos.x, transform.position.y);
            transform.Rotate(0f, 180f, 0f);
            isPlayerFacingRight = true;
        }
    }

    private void FaceTowardsMovement(Vector2 moveDirection)
    {
        float angle = Vector2.SignedAngle(Vector2.right, moveDirection);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ChasePlayer()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {

            Vector3 direction = (FindObjectOfType<PlayerController>().transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position);

            if (distanceToPlayer > 5f)
            {
                transform.position += direction * moveSpeed * Time.deltaTime;
                //FaceTowardsMovement(direction);
            }


            // Check if the enemy is facing away from the player
            if (Vector2.Dot(transform.right, direction) < 0f)
            {
                // Flip the enemy around to face the player
                transform.Rotate(0f, 180f, 0f);
            }

            // Fire arrows at the player at regular intervals
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    private void Shoot()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, ShootPos.position, Quaternion.identity);
            Vector2 direction = (FindObjectOfType<PlayerController>().transform.position - ShootPos.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;

            Vector3 lookDirection = arrow.GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
