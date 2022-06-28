using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyStartingSwordBehavior : BaseWeaponBehavior, IWeaponBehavior
{
    public int damageGiven = 2;
    public AudioClip swingSwordSound;

    private AudioSource audioSource;
    private BoxCollider2D collider2D;
    private Animator animator;

    public float timeOfAttack;
    private float attackClock;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        animator.SetBool("Attack", false);
        collider2D.enabled = false;
        attackClock = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!DialogueManager.GetInstance().dialogueIsPlaying)
            CheckForAttack();
    }

    private void FixedUpdate()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
            RotateWeapon();      
    }

    private void CheckForAttack()
    {
        //When left click of mouse active attack, possible damage, sound, and blood effects
        if (!animator.GetBool("Attack"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                float tempRoatationValue = gameObject.transform.localRotation.eulerAngles.z;
                if (tempRoatationValue > 180)
                    tempRoatationValue -= 360;

                animator.SetFloat("RotationValue", tempRoatationValue);
                animator.SetBool("Attack", true);
                attackClock = timeOfAttack;
            }

        }
        else if (attackClock > 0)
        {
            attackClock -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    //The below functions are called in the weapon animations

    private void PlaySwingSound()
    {
        audioSource.PlayOneShot(swingSwordSound);
    }

    private void TurnSwordColliderOn()
    {
        collider2D.enabled = true;
    }

    private void TurnSwordColliderOff()
    {
        collider2D.enabled = false;
    }

    public int ReturnWeaponDamage()
    {
        return damageGiven;
    }
}
