using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IEnemyState
{
    private EnemyAI enemy;
    private float attackTimer;
    private float attackAnimTimer;
    private float elaspedTime;
    private bool isAttacking;

    Collider2D enemyCollider;
    Collider2D weaponCollider;
    Vector2 previousPosition;

    public Attack(EnemyAI enemy)
    {
        this.enemy = enemy;
        enemyCollider = this.enemy.GetComponent<BoxCollider2D>();
        GameObject playerWeapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponCollider = playerWeapon != null ? playerWeapon.GetComponent<Collider2D>() : null;

        // 2 seconds between each attack
        attackTimer = 2.0f;

        // 0.25 seconds for attack animation
        attackAnimTimer = 0.25f;
    }

    public void StateEntered()
    {
        enemy.animator.SetBool("Moving", false);
        previousPosition = enemy.transform.position;
    }

    public void StateExit()
    { }

    public void StateUpdate()
    {
        elaspedTime += Time.deltaTime;

        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.attackingDistance)
        {
            enemy.ChangeState(new Move(enemy));
        }

        if (weaponCollider != null && enemyCollider.IsTouching(weaponCollider))
        {
            enemy.ChangeState(new Dodge(enemy));
        }

        // Check to see if 2 seconds have passed, if so trigger an attack and reset timer.
        if (elaspedTime >= attackTimer)
        {
            elaspedTime = 0f;
            Debug.Log("Player has been attacked!");
            isAttacking = true;
        }

        if (elaspedTime >= attackAnimTimer)
        {
            isAttacking = false;
        }

        // TODO Clean up on how the lerp is being used
        if (isAttacking)
        {
            enemyCollider.enabled = false;
            enemy.transform.position = Vector2.Lerp(previousPosition, enemy.player.transform.position, Mathf.PingPong(Time.time * enemy.speed, 0.35f));
        } else
        {
            enemyCollider.enabled = true;
        }
    }
}
