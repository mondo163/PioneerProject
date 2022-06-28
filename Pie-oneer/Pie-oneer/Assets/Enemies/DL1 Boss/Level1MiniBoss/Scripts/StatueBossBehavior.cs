using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StatueBossBehavior : MonoBehaviour
{
    public GameObject HealthBarCanvas;
    public HealthBar healthbar;
    public AudioSource transformationSound;
    public bool battleEnsuing;
    public int curHealth;

    private Animator animations;
    private UltraFireballWeapon ultraFireballs;
    private FireballWeapon fireballs;

    private bool firingActive;
    private bool takingDamage;
    private bool enragedState;
    private const int HEALTH = 40;
    
    // Start is called before the first frame update
    void Start()
    {
        curHealth = HEALTH;
        healthbar.PresetHealth(HEALTH);

        animations = this.GetComponent<Animator>();
        ultraFireballs = this.GetComponent<UltraFireballWeapon>();
        fireballs = this.GetComponent<FireballWeapon>();

        firingActive = false;
        enragedState = false;
        battleEnsuing = false;
        takingDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firingActive && battleEnsuing)
        {
            if (curHealth > (HEALTH / 2))
            {
                firingActive = true;
                StartCoroutine(AboveHalflifeAttackSequence());
            }
            
            else if (enragedState)
            {
                firingActive = true;
                StartCoroutine(BelowHalflifeAttackSequence());
            }
        }
        else if (curHealth == (HEALTH / 2) && !enragedState)
        {
            enragedState = true;
            StopAllStatueActions();
            StartCoroutine(EnragedStateChange());
        }
        else if (curHealth == 0 && battleEnsuing)
        {
            StopAllStatueActions();
        }

    }
   
    private void OnTriggerEnter2D(Collider2D HitInfo)
    {
        if (HitInfo.CompareTag("Weapon"))
        {
            if (!takingDamage && battleEnsuing) //makes sure the battle is ongoing and not already taking damage
            {
                //statue takes damage
                takingDamage = true;
                curHealth -= HitInfo.GetComponent<IWeaponBehavior>().ReturnWeaponDamage();
                healthbar.SetHealth(curHealth);

                //checks state of the statue for the correct hit marker
                if (enragedState)
                    animations.SetTrigger("EnragedStatueHit");
                else
                    animations.SetTrigger("StatueHit");

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            takingDamage = false;
        }
    }
   
    //coroutine that changes the statue state to enraged
    private IEnumerator EnragedStateChange()
    {

        //enraged animation is triggered
        yield return new WaitForSeconds(1f);
        animations.SetBool("Enraged", true);
        transformationSound.Play();
        yield return new WaitForSeconds(4f);

        //battle resumes 
        firingActive = false;
        battleEnsuing = true;
    }
    //stops all the attack sequences for halfway state change and the end of the battle. 
    private void StopAllStatueActions()
    {
        battleEnsuing = false;
        StopAllCoroutines();
        //manually destroys fireballs since StopAllCoroutines will not do it. 
        ultraFireballs.DestroyObjects();
    }
    //coroutine that controls the fireballs when your life is above the 
    //halfway point
    private IEnumerator AboveHalflifeAttackSequence()
    {
        yield return StartCoroutine(fireballs.ShootCounterClockwise());
        yield return StartCoroutine(fireballs.ShootClockwise());
        yield return StartCoroutine(ultraFireballs.ShootUltras());

        firingActive = false;
    }

    //coroutine that controls the fireball sequence when in the enraged state
    public IEnumerator BelowHalflifeAttackSequence()
    {
        
        yield return StartCoroutine(fireballs.ShootSimultaneously());
        yield return StartCoroutine(fireballs.ShootSimultaneously());

        yield return StartCoroutine(ultraFireballs.ShootUltras());
        yield return StartCoroutine(ultraFireballs.ShootUltras());

        firingActive = false;
    }
}
