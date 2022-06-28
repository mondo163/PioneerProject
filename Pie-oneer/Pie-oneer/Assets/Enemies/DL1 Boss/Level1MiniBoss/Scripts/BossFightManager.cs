using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossFightManager : MonoBehaviour
{
    public GameObject closingSceneManager;
    public GameObject openingSceneManager;
    public GameObject mainCamera;
    public GameObject boss;
    public GameObject mage;
    public GameObject dungeonDoorEntrance;
    public GameObject dungeonDoorExit;

    private Animator bossAnimations;
    private EdgeCollider2D cutsceneTrigger;
    private PlayableDirector closingDirector;
    private PlayableDirector openingDirector;
    private StatueBossBehavior bossBehavior;
    private bool endingTriggered;
    // Start is called before the first frame update
    void Start()
    {
        cutsceneTrigger = this.GetComponent<EdgeCollider2D>();
        openingDirector = openingSceneManager.GetComponent<PlayableDirector>();
        closingDirector = closingSceneManager.GetComponent<PlayableDirector>();
        bossBehavior = boss.GetComponent<StatueBossBehavior>();
        bossAnimations = boss.GetComponent<Animator>();
        endingTriggered = false;
        
    }

    private void Update()
    {
        if (bossBehavior.curHealth == 0 && !endingTriggered)
        {
            StartCoroutine(EndBossBattle());
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartBossBattle());
            cutsceneTrigger.enabled = false;//removes the collider for the cutscene
        }
    }
    //coroutine that is to start the main cutscene and start the battle
    private IEnumerator StartBossBattle()
    {
        //opening intro sequence
        openingDirector.Play();
        yield return new WaitForSeconds((float)openingDirector.duration + .25f);

        //camera animation and enabling collider so the player cannot exit the battle
        cutsceneTrigger.enabled = true;
        cutsceneTrigger.isTrigger = false;
        mainCamera.GetComponent<Animator>().SetTrigger("BossBattleBegins");
        yield return new WaitForSeconds(2f);

        //makes sure dunegon doors are closed and the mage vanishes
        dungeonDoorEntrance.GetComponent<DoorBehaviour>().isOpen = false;
        dungeonDoorExit.GetComponent<DoorBehaviour>().isOpen = false;
        mage.GetComponent<MageBehavior>().MageVanish();
        mage.GetComponent<Animator>().SetBool("MageInvisible", true);

        bossBehavior.battleEnsuing = true;
    }
    //Final cutscene and the door to the next dungeon opens. 
    private IEnumerator EndBossBattle()
    {
        //ending scene and opening door to next dungeon
        mage.SetActive(true);
        closingDirector.Play();
        bossAnimations.SetBool("StatueDead", true);
        yield return new WaitForSeconds((float)closingDirector.duration + .25f);

        endingTriggered = true;
        dungeonDoorExit.GetComponent<DoorBehaviour>().isOpen = true;
        mainCamera.GetComponent<Animator>().SetTrigger("BossBattleEnds");
    }
}
