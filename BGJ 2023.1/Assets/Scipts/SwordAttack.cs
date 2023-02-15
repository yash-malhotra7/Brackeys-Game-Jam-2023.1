using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Instances inst;

    public float attackDuration = 0.3f;
    private float attackTimer = 0;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        inst = GetComponentInParent<Instances>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackDuration)
        {
            isAttacking = false;
            if (Input.GetMouseButtonDown(0))
            {
                attackSword(inst.ShootPos);
                attackTimer = 0;
                isAttacking = true;
            }
        }
        inst.anim.SetBool("IsAttacking", isAttacking);
    }

    void attackSword(Transform ShootPos)
    {

        GameObject attackObj = Instantiate(inst.AttackPos, inst.ShootPos.position, Quaternion.identity).gameObject;
        Destroy(attackObj, attackDuration);
    }
}
