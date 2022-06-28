using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAction : Action
{
    public string saveFileName; 
    public BaseSM saveScene;

    public override void DoSomething()
    {
        saveScene.SaveToJSON(saveFileName);
    }
}
