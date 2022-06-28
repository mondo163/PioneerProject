using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour, IWeaponBehavior
{
    private Animator animator;
    public float timeToWait = 3;
    private float countDownTime;

    public AudioClip trapSound;
    private AudioSource audioSource;

    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        countDownTime = timeToWait;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countDownTime -= Time.deltaTime;
        if(countDownTime < 0 && !animator.GetBool("TimeIsDone"))
        {
            animator.SetBool("TimeIsDone", true);
        }
    }

    private void ResetTimeToWait()
    {
        countDownTime = timeToWait;
        animator.SetBool("TimeIsDone", false);
    }

    //For trap animation 
    public void PlayTrapSound()
    {
        audioSource.PlayOneShot(trapSound);
    }

    public int ReturnWeaponDamage()
    {
        //Do damage to player based off of damge variable
        Debug.Log("Player has been hit with spike trap");
        return damage;
    }
}
