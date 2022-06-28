using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MageBehavior : MonoBehaviour
{
    public GameObject markDialogue;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        markDialogue.GetComponent<TextMeshProUGUI>().text = "Mark the Destroyer: Your quest for vengeance ends today hero! Now you will die!";
    }

    // Update is called once per frame
    void Update()
    {
        //if mage is moving play footstep sound
        if (rb.velocity.magnitude != 0)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
    }
    public void MageVanish()
    {
        this.gameObject.SetActive(false);
        markDialogue.GetComponent<TextMeshProUGUI>().text = "Mark the Destroyer: I never thought I'd be beaten... may long live my legacy..";

    }

    
}
