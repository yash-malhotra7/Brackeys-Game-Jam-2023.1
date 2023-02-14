using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float TotalHealth = 100;

    private bool hasDied = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            float damageDealt =  collision.gameObject.GetComponent<Arrow>().arrowDamage;
            TotalHealth -= damageDealt;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if(TotalHealth <= 0 && !hasDied)
        {
            Die();
            hasDied = true;
        }
    }

    public void Die()
    {
        PatrolEnemy patrolEnemy = GetComponent<PatrolEnemy>();
        StaticEnemy staticEnemy = GetComponent<StaticEnemy>();
        PlayerController playerController = GetComponent<PlayerController>();
        if (patrolEnemy != null)
        {
            patrolEnemy.enabled = false;
        }

        if (staticEnemy != null)
        {
            staticEnemy.enabled = false;
        }

        if (playerController != null)
        {
            playerController.SpawnTheGhost();
            playerController.enabled = false;

        }
        gameObject.tag = "Untagged";
            Destroy(gameObject, 3f);
    }
}
