using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IEnemyState
{
    private EnemyAI enemy;

    public Idle(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void StateEntered()
    {
        enemy.animator.SetBool("Moving", false);
    }

    public void StateExit()
    { }

    public void StateUpdate()
    {
        if (enemy.IsPlayerDetected())
        {
            enemy.ChangeState(new Move(enemy));
        }
    }
}
