using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IEnemyState
{
    private EnemyAI enemy;

    public Move(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void StateEntered()
    {
        enemy.animator.SetBool("Moving", true);
    }

    public void StateExit()
    { }

    public void StateUpdate()
    {
        Vector3 playerDirection = enemy.transform.position - enemy.player.transform.position;
        Vector3 scale = enemy.transform.localScale;
        Vector3 playerPosition = enemy.player.transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        // Check if the player is detected and if the player is close enough to be attacked
        if (enemy.IsPlayerDetected() && Vector2.Distance(enemyPosition, playerPosition) > enemy.attackingDistance)
        {
            enemy.transform.position = Vector2.MoveTowards(enemyPosition, playerPosition, enemy.speed * Time.deltaTime);
        } 
        else if (enemy.IsPlayerDetected())
        {
            enemy.ChangeState(new Attack(enemy));
        }
        else
        {
            enemy.ChangeState(new Idle(enemy));
        }

        // Set direction enemy is facing based on player's direction
        if (playerDirection.x <= 0)
        {
            scale.x = 1.0f;
            enemy.transform.localScale = scale;
        } else
        {
            scale.x = -1.0f;
            enemy.transform.localScale = scale;
        }
    }
}
