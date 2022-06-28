using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayCoinAction : Action
{
    public int payCoin;
    public bool spawnsObject;
    public GameObject objectToSpawn;
    public GameObject smokeEffect;
    public AudioClip smokeEffectClip;
    public AudioSource audioSource;
    public float waitForSecs;
    private PlayerData playerData; // will get player coin amount

    public override void DoSomething()
    {
        //Will pay coin here
        PlayerHealth.GetPlayerHealth().gameObject.GetComponentsInChildren<Inventory>()[0].Coins -= payCoin;
        Debug.Log("Player paid " + payCoin + " coin/s!");
        
        if(spawnsObject)
        {
            StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject()
    {
        //Instantiate  effect on its position
        Instantiate(smokeEffect, gameObject.transform.position, Quaternion.identity);
        //Break Sound
        audioSource.PlayOneShot(smokeEffectClip);

        yield return new WaitForSeconds(waitForSecs);

        //Instantiate  objectToSpawn on its position
        Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
    }

}
