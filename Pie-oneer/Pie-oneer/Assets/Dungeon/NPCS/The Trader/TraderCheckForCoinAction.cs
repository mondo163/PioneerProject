using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderCheckForCoinAction : Action
{
    public Dialogue dialogue;
    public int coinAmount = 0;
    public List<CoinNeededToUnlock> coinNeededToUnlocks;
    public override void DoSomething()
    {
        // will get player coin amount
        coinAmount = PlayerHealth.GetPlayerHealth().gameObject.GetComponentsInChildren<Inventory>()[0].Coins;

        //check all the coinNeededToUnlocks
        UnlockAndLockChoices();

    }

    private void UnlockAndLockChoices()
    {
        foreach(CoinNeededToUnlock cntu in coinNeededToUnlocks)
        {
            if(coinAmount >= cntu.coinToUnlock)
            {
                bool isAlreadySet = false;

                //make sure choices do not already exist
                for (int i = 0; i < dialogue.messages[cntu.choicesMessageIndex].choices.Count; i++)
                {
                    List<string> currentChoices = dialogue.messages[cntu.choicesMessageIndex].choices[i].sameChoices; 
                    for(int k = 0; k < currentChoices.Count; k++)
                    {
                        if (currentChoices[k] == cntu.choices[0]) 
                        {
                            //choices are already there
                            isAlreadySet = true;
                            break;
                        }
                    }
                    if (isAlreadySet)
                        break;
                }

                //unlock and allow user to see choice
                if(!isAlreadySet)
                {
                    //allow user to see choices
                    Choice activateChoice = new Choice() { pathToNextMessage = cntu.pathToNextMessage, sameChoices = cntu.choices };
                    dialogue.messages[cntu.choicesMessageIndex].choices.Add(activateChoice);
                }
            }
            else
            {
                //lock and remove choices from dialogue
                bool isAlreadyRemoved = false;

                //make sure choices are removed
                for (int i = 0; i < dialogue.messages[cntu.choicesMessageIndex].choices.Count; i++)
                {
                    List<string> currentChoices = dialogue.messages[cntu.choicesMessageIndex].choices[i].sameChoices;
                    for (int k = 0; k < currentChoices.Count; k++)
                    {
                        if (currentChoices[k] == cntu.choices[0])
                        {
                            //choices are already there
                            isAlreadyRemoved = true;
                            dialogue.messages[cntu.choicesMessageIndex].choices.RemoveAt(i);
                            break;
                        }
                    }
                    if (isAlreadyRemoved)
                        break;
                }
            }
        }
    }
}

[System.Serializable]
public class CoinNeededToUnlock
{

    //represents index of the next message (so choices can map to other messages)
    public int coinToUnlock;
    [Header("It's Message Index")]
    public int choicesMessageIndex;
    [TextArea(3, 10)]
    public List<string> choices;
    [Header("To Message Index")]
    public int pathToNextMessage;
}
