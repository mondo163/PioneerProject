using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueAttack : IEnemyState
{
    private StatueMiniBossAI statue;
    private float timeBetweenShots;
    private Transform player;
    private Transform firepoint;
    private int count = 0;
    public StatueAttack(StatueMiniBossAI statue, float timeBetweenShots)
    {
        this.statue = statue;
        this.timeBetweenShots = timeBetweenShots;
        this.player = statue.player;
        this.firepoint = statue.fireTrans;
    }

    public void StateEntered()
    {
        
    }

    public void StateExit()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        if (!statue.IsPlayerDetected())
        {
            statue.ChangeState(new StatueIdle(statue));
        }

        if (statue.health <= 0)
        {
            statue.ChangeState(new StatueDead(statue));
        }

        //calculate
        if (count == 0)
        {
            float horz = player.position.x - firepoint.position.x;
            float vert = player.position.y - firepoint.position.y;
            float hypo = Mathf.Sqrt((vert * vert) + (horz * horz));

            float omega = Mathf.Acos(horz / hypo);
            float omega2 = Mathf.Acos(vert / hypo);


            statue.fireball.ShootSinglesAtPlayer(omega);
            statue.fireball.ShootSinglesAtPlayer(omega2);

            count++;
        }
    }
}
