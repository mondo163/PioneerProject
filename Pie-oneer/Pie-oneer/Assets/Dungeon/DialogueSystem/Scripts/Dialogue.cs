using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Image speakerImage;
    public List<Message> messages;

    private bool isInRange;
    private Color startColorParent;
    // Start is called before the first frame update
    void Start()
    {
        startColorParent = GetComponentInParent<Renderer>().material.color;
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            GetComponentInParent<Renderer>().material.color = Color.yellow;
            //if 'f' is clicked then call start conversation
            if (Input.GetKeyDown("f"))
                StartConversation();
        }
        else
            GetComponentInParent<Renderer>().material.color = startColorParent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")  
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isInRange = false;
    }

    public void StartConversation()
    {
        //call DialogueManager and that class will handle the dialogue
        DialogueManager.GetInstance().StartDialogue(this);
    }
}

[System.Serializable]
public class Message
{
    //A message CAN have actions, leave empty for no action
    public List<Action> actions;
    //for long dialogue
    [TextArea(3,10)]
    public List<string> wholeMessage;
    //a list of choices and some choices result in the same path 
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    //represents index of the next message (so choices can map to other messages)
    [Header("Message Element Index")]
    public int pathToNextMessage;
    [TextArea(3, 10)]
    public List<string> sameChoices;
}


