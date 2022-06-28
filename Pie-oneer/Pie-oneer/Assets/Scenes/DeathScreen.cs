using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Button mainMenu;
    public Button exitGame;

    private void Awake()
    {
        //Delete DontDestroy Objects
        DontDestroyOnLoadManager.DestroyAll();
    }

    void Start()
    {
        mainMenu.onClick.AddListener(GoToMainMenu);
        exitGame.onClick.AddListener(ExitGame);
    }
    //main menu click
    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
