using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRiddleAction : Action
{
    public GhostRiddleManager ghostRiddleManager;
    public GameObject ghostDialogue;
    public int riddleNum;
    public bool wasCorrect;
    public bool isActivated;
    public float waitInSecForNext;
    public List<GameObject> spawnedObjects; //When Null then allow next dialogue trigger And then Destroy 
    public DoorBehaviour door;
    public AudioClip enemySoundClip;
    public AudioClip smokeEffectClip;
    public AudioSource audioSource;

    private bool startedCallingManager;
    private bool activatedSpawnObjects;
    private bool finishedCallingManager;
    // Start is called before the first frame update
    void Start()
    {
        startedCallingManager = false;
        finishedCallingManager = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated || DialogueManager.GetInstance().dialogueIsPlaying)
            return;

        //first time out of dialogue
        if(!activatedSpawnObjects)
        {
            //Destroy dialogue Trigger
            Destroy(ghostDialogue);
            activatedSpawnObjects = true;
            StartCoroutine(ActivateChildren());
        }

        //check to see if spawned objects is NULL or answer was correct
        if (!startedCallingManager && (wasCorrect || AllSpawnedObjectsDestroyed()))
        {
            startedCallingManager = true;
            //Update Manager
            StartCoroutine(UpdateGhostRiddleManager());
        }

        if (AllSpawnedObjectsDestroyed() && finishedCallingManager)
        {
            if (!wasCorrect)
                door.isOpen = true;

            isActivated = false;

            //if all objects destoryed then no need for game objects for that Riddle
            Destroy(gameObject.transform.parent.gameObject, 2);
        }
    }

    public override void DoSomething()
    {
        //set as active
        isActivated = true;

        if (!wasCorrect)
        {
            door.isOpen = false;
            audioSource.PlayOneShot(enemySoundClip);
        }
    }

    private IEnumerator UpdateGhostRiddleManager()
    {
        yield return new WaitForSeconds(waitInSecForNext);

        //call manager
        ghostRiddleManager.NextDialogue(riddleNum, wasCorrect);

        finishedCallingManager = true;
    }

    private IEnumerator ActivateChildren()
    {
        audioSource.PlayOneShot(smokeEffectClip);
        //activate first child first (Smoke) then all other children
        if (transform.GetChild(0) != null)
            transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds((float).2);

        //activate all children
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private bool AllSpawnedObjectsDestroyed()
    {
        bool allDestroyed = true;

        foreach(GameObject go in spawnedObjects)
        {
            if(go != null)
            {
                allDestroyed = false;
                break;
            }
        }

        return allDestroyed;
    }
}
