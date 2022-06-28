using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlePotionAction : Action
{
    //Game Objects
    public List<GameObject> otherPotions;
    public GameObject currentPotion;
    public GameObject potionExplodeEffect;

    //sounds
    public AudioClip drinkSound;
    public AudioClip reactionSound;
    public AudioClip breakSound;
    public AudioClip healthChangingSound;

    //public data Types
    public float destroyTime;
    public bool isGoodPotion;
    public float waitToPlayReactionInSeconds;
    public float waitForOutcomeInSeconds;
    public float waitToDestoryNextPotion;
    public float waitToStartDestroyingPotions;

    //private variables
    private bool destroyOtherPotions;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if dialogue is not playing and this potion was picked
        if (!DialogueManager.GetInstance().dialogueIsPlaying && destroyOtherPotions)
        {
            //destroy other potions 
            destroyOtherPotions = false;
            //Call Reaction
            StartCoroutine(Reaction());
        }
    }
    public override void DoSomething()
    {
        Debug.Log("A Potion has been drank");

        //Make sure other potions get destroyed after leaving dialogue
        destroyOtherPotions = true;

        //Call DrinkCurrentPotion
        StartCoroutine(DrinkCurrentPotion());
    }

    //Other Potions will be destroyed with particle effect
    private IEnumerator DestroyOtherPotions()
    {
        foreach (GameObject go in otherPotions)
        {
            //Instantiate  effect on its position
            Instantiate(potionExplodeEffect, go.transform.position, Quaternion.identity);
            //Play Break Sound
            audioSource.PlayOneShot(breakSound);
            //Add in coins, potions, when destroyed
            Destroy(go, destroyTime);

            //wait to destroy the next one
            yield return new WaitForSeconds(waitToDestoryNextPotion);
        }
    }

    //Destroy current potion and then wait seconds until outcome
    private IEnumerator DrinkCurrentPotion()
    {
        //Play drinking sound
        audioSource.PlayOneShot(drinkSound);

        //Remove potion here 
        Destroy(currentPotion);

        yield return new WaitForSeconds(waitToStartDestroyingPotions);

        //Call Destory other Potions
        StartCoroutine(DestroyOtherPotions());

    }

    private IEnumerator Reaction() //TODO
    {
        //wait a certain amount of time before playing reaction sound
        yield return new WaitForSecondsRealtime(waitToPlayReactionInSeconds);

        //Play reaction sound effect
        audioSource.PlayOneShot(reactionSound);

        //wait a certain amount of time
        yield return new WaitForSecondsRealtime(waitForOutcomeInSeconds);

        //Play Health Change sound
        audioSource.PlayOneShot(healthChangingSound);

        //Give the health effect (plus one heart or minus one)
        if (isGoodPotion)
        {
            //Change max health
            PlayerHealth.GetPlayerHealth().MaxHealth += 1;
            //Change HeartContainers
            PlayerHealth.GetPlayerHealth().HeartContainers = PlayerHealth.GetPlayerHealth().MaxHealth;
            //heal for 1
            PlayerHealth.GetPlayerHealth().Heal(1);
        }
        else
        {
            //Change max health
            PlayerHealth.GetPlayerHealth().MaxHealth -= 1;
            //Change HeartContainers
            PlayerHealth.GetPlayerHealth().HeartContainers = PlayerHealth.GetPlayerHealth().MaxHealth;
            //lose 1 health if at full before losing max
            if(PlayerHealth.GetPlayerHealth().health == (PlayerHealth.GetPlayerHealth().MaxHealth + 1))
                PlayerHealth.GetPlayerHealth().Damage(1);
        }
    }
}
