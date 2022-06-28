using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMg : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
            else {
                if (exitPanel)
                {
                    exitPanel.SetActive(true);
                }
            }
        }
    }

    public void UserClickYesNo(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }
        exitPanel.SetActive(false);
    }
}
