using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    PlayerController playerController;

    public GameObject arrowPrefab;
    public float arrowSpeed = 10.0f;
    public Transform arrowSpawnPoint;
    public float timeBetweenShots = 1.0f;

    private float timeSinceLastShot = 0.0f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timeSinceLastShot >= timeBetweenShots)
        {
            ShootArrow();
            timeSinceLastShot = 0.0f;
        }
    }

    void ShootArrow()
    {
        if (playerController.isPlayerFacingRight)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
            float player_z = playerController.transform.localScale.z;
            arrowRigidbody.velocity = arrowSpeed * arrowSpawnPoint.right;

            Vector3 lookDirection = arrowRigidbody.velocity.normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
            float player_z = playerController.transform.localScale.z;
            arrowRigidbody.velocity = -arrowSpeed * arrowSpawnPoint.right;

            Vector3 lookDirection = arrowRigidbody.velocity.normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
