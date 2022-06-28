using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderChangeDialogueAction : Action
{
    public int nextDialogue;
    public float waitInSecForNext;
    public List<GameObject> dialogueTriggersToDelete;

    public TraderManager traderManager;

    public override void DoSomething()
    {
        StartCoroutine(UpdateGhostRiddleManager());
    }

    private IEnumerator UpdateGhostRiddleManager()
    {

        //call manager
        traderManager.ActivateNextDiaglogue(nextDialogue);

        yield return new WaitForSeconds(waitInSecForNext);

        foreach(GameObject go in dialogueTriggersToDelete)
        {
            if(go != null)
                Destroy(go);
        }
    }
}
