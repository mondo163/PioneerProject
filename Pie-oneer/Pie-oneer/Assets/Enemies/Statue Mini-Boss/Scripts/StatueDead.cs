using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDead: IEnemyState
{
    private StatueMiniBossAI statue;
    public StatueDead(StatueMiniBossAI statue)
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
    }
    private IEnumerator StatueDeathActions()
    {
        statue.animator.SetBool("StatueDead", true);
        statue.transformingSFX.Play();
        statue.DropItems();
        yield return new WaitForSeconds(1.75f);
    }
}
