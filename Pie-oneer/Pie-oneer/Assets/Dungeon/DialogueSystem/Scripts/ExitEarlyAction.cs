using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEarlyAction : Action
{
    public override void DoSomething()
    {
        DialogueManager.GetInstance().ExitDialogue();
    }
}
