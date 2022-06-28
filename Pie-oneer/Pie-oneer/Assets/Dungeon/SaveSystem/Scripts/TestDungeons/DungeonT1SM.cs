using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonT1SM : BaseSM
{
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealth.GetPlayerHealth();
        alreadySaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SavingManager.Instance.isLoaded && !alreadySaved)
        {
            alreadySaved = true;
            Load();
        }
    }

    public override void Load()
    {
        //Loads in all enemies that are 
        LoadEnemies(SavingManager.Instance.savedData.dungeonT1Data.enemiesData);


        //Player only needs to be loaded once when game starts
        if (!SavingManager.Instance.isPlayerLoaded)
        {
            player = PlayerHealth.GetPlayerHealth();
            LoadPlayer(SavingManager.Instance.savedData.playerData);
            SavingManager.Instance.isPlayerLoaded = true;
        }
    }

    public override void SaveToSavedData()
    {
        //Save the enemy data for the dungeonT1 Scene
        SavingManager.Instance.savedData.dungeonT1Data.enemiesData  = GetEnemies();

        //other data regarding scene will go below
        
    }

    public override void SaveToJSON(string nameSaveFile)
    {
        SaveToSavedData();
        SavingManager.Instance.savedData.playerData = GetPlayerData();
        SavingManager.Instance.savedData.currentScene = sceneName;
        SavingManager.Instance.SaveToJSON(nameSaveFile);
    }
}
