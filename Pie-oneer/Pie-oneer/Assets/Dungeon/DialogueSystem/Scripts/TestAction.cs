using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : Action
{
    public bool turnGreen;
    // Start is called before the first frame update
    void Start()
    {
        turnGreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(turnGreen && !DialogueManager.GetInstance().dialogueIsPlaying)
            //TurnGreen();
    }

    public override void DoSomething()
    {
        turnGreen = true;
        TurnGreen();
    }

    private void TurnGreen()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

}
