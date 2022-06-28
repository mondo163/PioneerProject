using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject boss;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (exitPanel.activeInHierarchy == false)
            {
                Time.timeScale = 0;
                SetMenuPosition();
                exitPanel.SetActive(true);
            }
            
        }
        if (exitPanel.activeInHierarchy == true)
        {
            SetMenuPosition();
        }
    }

    public void UserClickYesNo_ReturnMainMenu(int choice)
    {
        if (choice == 1)
        {
            GameObject canvas = player.GetComponent<GameObject>();
            SceneManager.LoadScene(0);
        }
        exitPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void UserClickYesNo_ExitGame(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }
        exitPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetMenuPosition()
    {
        bool bossBattleActive = boss.GetComponent<StatueBossBehavior>().battleEnsuing;
        GameObject pb = (bossBattleActive == true) ? boss : player;
        if (pb == null) return;

        Vector3 playerCords = pb.transform.position;
        playerCords.x -= .2f;
        playerCords.y -= .1f;

        exitPanel.transform.position = playerCords;
    }

    private IEnumerator WaitToUnPause()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
    }
}
