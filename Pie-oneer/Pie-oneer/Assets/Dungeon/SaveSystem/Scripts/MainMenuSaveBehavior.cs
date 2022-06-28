using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSaveBehavior : MonoBehaviour
{
    public GameLoader gameLoader;
    public GameObject[] gameSaveSlots;
    private void Start()
    {
        SetGameSaveSlotStatus();
    }

    private void SetGameSaveSlotStatus()
    {
        CheckAndSetSingleSave(Application.dataPath + "/StreamingAssets/Saves/save1.json", 0, "Save #1");
        CheckAndSetSingleSave(Application.dataPath + "/StreamingAssets/Saves/save2.json", 1, "Save #2");
        CheckAndSetSingleSave(Application.dataPath + "/StreamingAssets/Saves/save3.json", 2, "Save #3");
    }

    private void CheckAndSetSingleSave(string fileName, int indexSaveSlot, string slotText)
    {
        if (File.Exists(fileName))
            gameSaveSlots[indexSaveSlot].GetComponentInChildren<TextMeshProUGUI>().text = slotText;
        else
        {
            //disable click function
            gameSaveSlots[indexSaveSlot].GetComponent<Button>().onClick = null;
            return;
        }
    }

    public void LoadSelectedSave(string saveFileName)
    {
        //load save data
        SavingManager.Instance.LoadFromJSON(saveFileName);
        //transition to next scene
        gameLoader.LoadNextLevel(SavingManager.Instance.savedData.currentScene);
    }

    public void LoadNewGame()
    {
        //load save data
        SavingManager.Instance.LoadFromJSON("saveReset");
        //transition to Intro Cutscene
        gameLoader.LoadNextLevel("Intro Cutscene");
    }
}
