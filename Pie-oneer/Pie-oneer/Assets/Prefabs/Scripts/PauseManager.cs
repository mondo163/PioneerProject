using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject exitConfirm;
    public GameObject player;
    public GameObject boss;

    private bool PauseMenuActive = false;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = (!PauseMenuActive) ? 0 : 1;
            
            pauseMenu.SetActive(!PauseMenuActive); 
            PauseMenuActive = !PauseMenuActive;

            SetMenuPosition();
        }
    }
    public void Unpause()
    {
        PauseMenuActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetMenuPosition()
    {
        bool bossBattleActive = boss.GetComponent<StatueBossBehavior>().battleEnsuing;
        GameObject pb = (bossBattleActive == true) ? boss : player;
        if (pb == null) return;

        Vector3 objectCords = pb.transform.position;
        objectCords.x -= .2f;
        objectCords.y -= .1f;

        pauseMenu.transform.position = objectCords;
        settingsMenu.transform.position = objectCords;
        exitConfirm.transform.position = objectCords;
    }

    private IEnumerator WaitToUnPause()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
    }
}
