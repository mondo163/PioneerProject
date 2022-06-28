using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitManagerMainMenu : MonoBehaviour
{
    public GameObject exitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (exitPanel.activeInHierarchy == false)
            {
                exitPanel.SetActive(true);
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
