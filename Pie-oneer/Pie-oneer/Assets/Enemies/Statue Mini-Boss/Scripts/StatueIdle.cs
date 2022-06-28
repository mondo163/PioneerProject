using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueIdle : IEnemyState
{
    private StatueMiniBossAI statue;
    public StatueIdle(StatueMiniBossAI statue)
    {
        this.statue = statue;
    }
    public void StateEntered()
    {     
    }

    public void StateExit()
    {
    }

    public void StateUpdate()
    {
        if (statue.IsPlayerDetected())
        {
            MonoBehaviour.Instantiate(statue).StartCoroutine(StatueAttackStateChangeActions());
        }
    }

    private IEnumerator StatueAttackStateChangeActions()
    {
        if (!statue.animator.GetBool("PlayerDetected"))
        {
            statue.animator.SetBool("PlayerDetected", true);
            statue.transformingSFX.Play();
        }
        yield return new WaitForSeconds(1.1f);
        statue.ChangeState(new StatueAttack(statue, statue.timeBetweenShots));
    }
}
