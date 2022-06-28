using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    //UI Elements that need to be turned off during dialogue
    public List<GameObject> turnOffObjects = new List<GameObject>();

    //All Dialogue Box UI elements
    public GameObject dialogueBox;
    public RectTransform contentRectTransform;
    public List<GameObject> buttons = new List<GameObject>();
    public TextMeshProUGUI dialogueMessage;
    public Image characterImage;
    public ScrollRect scrollRect;

    //Sounds
    public AudioClip buttonClickedSound;
    private AudioSource audioSource;

    //this dictionary will map button indexs to choices that the button text is connected to
    private Dictionary<int, int> buttonIndexToChoices = new Dictionary<int, int>();
    private Dialogue currentDialogue;
    public bool dialogueIsPlaying { get; private set; }
    private int currentMessageIndex;
    private int currentWholeMessageIndex;
    private float buttonScrollContentHeightPerButton;
    private float buttonScrollContentWidth;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        buttonScrollContentHeightPerButton = (contentRectTransform.sizeDelta.y * contentRectTransform.localScale.x) / buttons.Count;
        buttonScrollContentWidth = contentRectTransform.sizeDelta.x * contentRectTransform.localScale.x;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //de-activate all game objects that need to be turned off
        TurnOffOrOnGameObjects(true);

        currentDialogue = dialogue;
        dialogueIsPlaying = true;
        dialogueBox.SetActive(true);
        characterImage.sprite = dialogue.speakerImage.sprite;
        characterImage.color = dialogue.speakerImage.color;

        //Start dialogue off with message at 0th index
        GoToNextMessage(0);
    }

    public void ChosenChoice(int index)
    {
        //this unselects the button that was clicked allowing for highlighting when hovered over
        EventSystem.current.SetSelectedGameObject(null);

        //Reset scrollbar back to top of the contents
        scrollRect.verticalNormalizedPosition = 1;

        //play sound for when button is clicked
        audioSource.PlayOneShot(buttonClickedSound);

        if (currentDialogue.messages[currentMessageIndex].choices.Count == 0 && !(currentWholeMessageIndex < currentDialogue.messages[currentMessageIndex].wholeMessage.Count))
        {
            ExitDialogue();
            return;
        }
        else if (buttonIndexToChoices.Count == 0)
        {
            StartCoroutine(ContinueDisplayOfWholeMessage());
            return;
        }

        GoToNextMessage(buttonIndexToChoices[index]);
    }

    private IEnumerator ContinueDisplayOfWholeMessage()
    {
        //deactivate first button
        buttons[0].SetActive(false);

        //Displays the message with letters slowly being put into dialogue box
        //start the sound of NPC talking and then after dialogue is down being written then stop sound
        audioSource.Play();
        yield return StartCoroutine(TypeCurrentSegmentMessage());
        audioSource.Stop();
        //wait after dialogue is done
        yield return new WaitForSecondsRealtime((float)0.2);

        DisplayContinueChoice();

        if (currentDialogue.messages[currentMessageIndex].wholeMessage.Count - 1 == currentWholeMessageIndex)
            DisplayChoices();
        
        currentWholeMessageIndex++;
    }

    private IEnumerator TypeCurrentSegmentMessage()
    {
        //clear dialogue text
        dialogueMessage.text = "";
        //grab current message segment
        string messageSegment = currentDialogue.messages[currentMessageIndex].wholeMessage[currentWholeMessageIndex];

        foreach (char letter in messageSegment)
        {
            dialogueMessage.text += letter;

            //wait 0.025 seconds then put next letter
            yield return new WaitForSecondsRealtime((float)0.025);
        }
    }

    private void DisplayChoices()
    {
        //check and activate current Messages Actions
        if (currentDialogue.messages[currentMessageIndex].actions.Count != 0)
            ActivateCurrentMessageActions();

        //if there are no choices allow user to end dialogue with button
        if (currentDialogue.messages[currentMessageIndex].choices.Count == 0)
        {
            //activate and set text for end button
            buttons[0].SetActive(true);
            buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "end";
            //change content height of the scroll view according to one button
            contentRectTransform.sizeDelta = new Vector2(buttonScrollContentWidth, buttonScrollContentHeightPerButton);
            return;
        }

        //Find all choices there are
        Dictionary<string, int> tempChoices = new Dictionary<string, int>();
        foreach (var choice in currentDialogue.messages[currentMessageIndex].choices)
        {
            for (int i = 0; i < choice.sameChoices.Count; i++)
            {
                tempChoices.Add(choice.sameChoices[i], choice.pathToNextMessage);
            }
        }

        int buttonNextIndex = 0;
        int totalChoices = tempChoices.Count;
        List<string> choices = new List<string>(tempChoices.Keys);
        //Assign choices to buttons and add to buttonIndexToChoices dictionary
        for (int i = totalChoices; i > 0 && buttonNextIndex < buttons.Count; i--)
        {
            //grab random index to randomize choices
            string temp = choices[Random.Range(0,i)];
            //then remove choice from list
            choices.Remove(temp);
            //add to buttonIndexToChoices buttonIndex->choice.pathToNextMessage
            buttonIndexToChoices[buttonNextIndex] = tempChoices[temp];
            //make the button active 
            buttons[buttonNextIndex].SetActive(true);
            //set text to that button
            buttons[buttonNextIndex].GetComponentInChildren<TextMeshProUGUI>().text = temp;
            buttonNextIndex++;
        }

        // change content height of the scroll view according to total in use buttons
        contentRectTransform.sizeDelta = new Vector2(buttonScrollContentWidth, buttonScrollContentHeightPerButton* totalChoices);
    }

    //Goes to next message and resets variables 
    private void GoToNextMessage(int indexMessageID)
    {
        //go to the next message based off of indexID
        currentMessageIndex = indexMessageID;
        //Reset the current whole message index
        currentWholeMessageIndex = 0;
        //clear the dictionary
        buttonIndexToChoices.Clear();

        //deactivate all buttons
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(false);
        }

        //start the display of the new message
        StartCoroutine(ContinueDisplayOfWholeMessage());
    }

    //displays only the continue button to net part of the current message
    private void DisplayContinueChoice()
    {
        //make the button active 
        buttons[0].SetActive(true);
        //change content height of the scroll view  according to one button
        contentRectTransform.sizeDelta = new Vector2(buttonScrollContentWidth, buttonScrollContentHeightPerButton);
        //set the button's text to continue
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "continue";
    }
    //can be accessed by outside scripts
    public void ExitDialogue()
    {
       //Reset variables and deactivate dialogue box
        currentDialogue = null;
        dialogueIsPlaying = false;
        buttonIndexToChoices.Clear();
        dialogueBox.SetActive(false);

        //activate all other game objects that were turned off
        TurnOffOrOnGameObjects(false);
    }

    private void ActivateCurrentMessageActions()
    {
        foreach (Action action in currentDialogue.messages[currentMessageIndex].actions)
        {
            action.DoSomething();
        }
    }

    //Activates or un activate game objects that aren't suppose to be on during dialogue
    private void TurnOffOrOnGameObjects(bool turnOff)
    {
        if(turnOff)
        {
            foreach (GameObject go in turnOffObjects)
            {
                go.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject go in turnOffObjects)
            {
                go.SetActive(true);
            }
        }
    }
}
