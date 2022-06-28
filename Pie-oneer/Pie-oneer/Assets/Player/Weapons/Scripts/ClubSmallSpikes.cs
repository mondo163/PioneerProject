using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubSmallSpikes : BaseWeaponBehavior
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
            RotateWeapon();      
    }
}
