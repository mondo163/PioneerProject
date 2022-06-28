using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    
    public void LoadNextLevel(string sceneName)
    {
       StartCoroutine(LoadLevel(sceneName));
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //loadScene
        SceneManager.LoadScene(sceneName);
    }
}
