using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public float destroyTime;
    public GameObject breakEffect;
    public AudioClip breakSound;
    public bool isSpecialSpawn;
    public GameObject specialSpawn;

    private AudioSource audioSource;
    private bool isBreaking;

    //Class that takes care of spawning objects
    public ObjectSpawner objectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isBreaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && !isBreaking)
        {
            isBreaking = true;

            //Instantiate  effect on its position
            Instantiate(breakEffect, gameObject.transform.position, Quaternion.identity);
            //Break Sound
            audioSource.PlayOneShot(breakSound);
            //Spawn object, if there is no objectSpawner spawn nothing
            if (!isSpecialSpawn && objectSpawner != null)
                StartCoroutine(objectSpawner.SpawnObject(gameObject.transform.position, Quaternion.identity, (float)(destroyTime - 0.1)));
            else if (isSpecialSpawn && specialSpawn != null)
                StartCoroutine(SpawnSpecialObject());
            //Destroy barrel
            Destroy(gameObject, destroyTime);
        }    
    }

    private IEnumerator SpawnSpecialObject()
    {
        yield return new WaitForSeconds((float)(destroyTime - 0.1));

        Instantiate(specialSpawn, gameObject.transform.position, Quaternion.identity);
    }
}
