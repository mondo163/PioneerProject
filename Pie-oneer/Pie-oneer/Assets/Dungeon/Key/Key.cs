using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound;
    public DoorBehaviour door;
    public string name;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Make sure that key that the door goes to knows 
            if (door != null)
                door.hasKey = true;
            //make pickup sound
            audioSource.PlayOneShot(pickupSound);
            //destroy Key's parent
            Destroy(gameObject.GetComponentInParent<Transform>().gameObject, (float)3.5);
            //destroy key
            Destroy(gameObject);
        }
    }
}
