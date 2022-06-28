using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : Action
{
    public AudioClip doorSound;
    private AudioSource audioSource;

    private Animator animator;

    public bool isOpen;
    public bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if((isOpen && !animator.GetBool("IsOpen")) || (!isOpen && animator.GetBool("IsOpen")))
        {
            animator.SetBool("IsOpen", isOpen);
        }
    }

    public void DoorSound()
    {
        audioSource.PlayOneShot(doorSound);
    }

    public override void DoSomething()
    {
        //if player has key to door then open door 
        if(hasKey)
        {
            isOpen = true;

            //if called turn off trigger to interact with door (is the only child)
            GameObject dialogueTrigger = gameObject.transform.GetChild(0).gameObject;
            dialogueTrigger.SetActive(false);
        }
    }
}
