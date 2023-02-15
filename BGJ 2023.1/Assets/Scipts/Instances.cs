using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instances : MonoBehaviour
{
    public GameObject GhostPrefab;
    public GameObject arrowPrefab;
    public Animator anim;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpTime;
    public float checkRadius;
    public float jumpVelocityReductionRate = 0.1f;
    public float ghostSpawnTime = 1f;
    public float arrowSpeed = 10f;
    public Transform arrowSpawnPoint;
    public float timeBetweenShots = 1.0f;
    public bool isPlayerFacingRight = true;
    public LayerMask whatIsGround;
    public Transform BottomPos;
    public Transform ShootPos;
    public Transform AttackPos;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PatrolEnemy enemy = GetComponent<PatrolEnemy>();

        if(enemy != null)
        {
            isPlayerFacingRight = enemy.isPlayerFacingRight;
        }
    }
}
