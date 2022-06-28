using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneToD1 : MonoBehaviour
{
    public GameLoader gameLoader;
    private void Awake()
    {
        //transition to next scene
        gameLoader.LoadNextLevel(SavingManager.Instance.savedData.currentScene);
    }
}
