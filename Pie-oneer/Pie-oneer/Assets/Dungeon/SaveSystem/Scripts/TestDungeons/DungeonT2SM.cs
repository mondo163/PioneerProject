using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonT2SM : BaseSM
{
    //data
    public GameObject blockCanBeGreen;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealth.GetPlayerHealth();
        alreadySaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SavingManager.Instance.isLoaded && !alreadySaved)
        {
            alreadySaved = true;
            Load();
        }
    }

    public override void Load()
    {
        //Loads in all enemies that are 
        LoadEnemies(SavingManager.Instance.savedData.dungeonT2Data.enemiesData);

        //other data regarding scene will go below
        if(SavingManager.Instance.savedData.dungeonT2Data.blockIsGreen)
            blockCanBeGreen.GetComponent<TestAction>().DoSomething();

        //Player only needs to be loaded once when game starts
        if(!SavingManager.Instance.isPlayerLoaded)
        {
            player = PlayerHealth.GetPlayerHealth();
            LoadPlayer(SavingManager.Instance.savedData.playerData);
            SavingManager.Instance.isPlayerLoaded = true;
        }   
    }

    public override void SaveToJSON(string nameSaveFile)
    {
        SaveToSavedData();
        SavingManager.Instance.savedData.playerData = GetPlayerData();
        SavingManager.Instance.savedData.currentScene = sceneName;
        SavingManager.Instance.SaveToJSON(nameSaveFile);
    }

    public override void SaveToSavedData()
    {
        //Save the enemy data for the dungeonT1 Scene
        SavingManager.Instance.savedData.dungeonT2Data.enemiesData = GetEnemies();

        //other data regarding scene will go below
        SavingManager.Instance.savedData.dungeonT2Data.blockIsGreen = blockCanBeGreen.GetComponent<TestAction>().turnGreen;

    }
}
