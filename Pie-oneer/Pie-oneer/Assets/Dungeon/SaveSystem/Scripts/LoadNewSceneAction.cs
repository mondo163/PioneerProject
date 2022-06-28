using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneAction : Action
{
    public GameLoader gameLoader;
    public BaseSM currentSM;
    public float playerNewPosX;
    public float playerNewPosY;
    public string sceneName;
    public override void DoSomething()
    {
        //exit dialogue
        DialogueManager.GetInstance().ExitDialogue();
        //save currrent dungeon data to saveData (not to JSON)
        currentSM.SaveToSavedData();
        StartCoroutine(WaitToMovePlayer());
        //transition to next scene
        gameLoader.LoadNextLevel(sceneName);
    }

    private IEnumerator WaitToMovePlayer()
    {

        yield return new WaitForSeconds(gameLoader.transitionTime);

        //Move Player to new Pos
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>().position = new Vector3(playerNewPosX, playerNewPosY, 0);
    }
}
