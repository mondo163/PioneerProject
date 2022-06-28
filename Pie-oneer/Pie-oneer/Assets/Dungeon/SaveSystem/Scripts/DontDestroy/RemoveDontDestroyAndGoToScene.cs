using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDontDestroyAndGoToScene : Action
{
    public GameLoader gameLoader;
    public string sceneName;
    public override void DoSomething()
    {
        //exit dialogue
        DialogueManager.GetInstance().ExitDialogue();
        StartCoroutine(WaitToRemoveDontDestroy());
        //transition to next scene
        gameLoader.LoadNextLevel(sceneName);
    }

    private IEnumerator WaitToRemoveDontDestroy()
    {

        yield return new WaitForSeconds(gameLoader.transitionTime);

        //Delete DontDestroy Objects
        DontDestroyOnLoadManager.DestroyAll();
    }
}
