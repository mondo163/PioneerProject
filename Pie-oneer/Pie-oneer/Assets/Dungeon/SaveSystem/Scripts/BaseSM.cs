using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSM : MonoBehaviour
{
    protected bool alreadySaved;
    //String for dungeon name in scene manager
    public string sceneName;
    //Gets player health
    protected PlayerHealth player;
    //Gameobjects of all the enemies that must be recorded in scene
    public GameObject[] enemies;
    //Load all player data to scene
    protected void LoadPlayer(PlayerData playerData)
    {
        player.MaxHealth = playerData.heartsMax;
        player.HeartContainers = playerData.heartsMax;
        player.health = playerData.currentHealth;
        player.GetComponentInParent<Transform>().position = new Vector3(playerData.currentPosX, playerData.currentPosY, 0);
    }
    //Save all player data to scene
    protected PlayerData GetPlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.currentHealth = player.health;
        playerData.heartsMax = player.MaxHealth;
        playerData.currentPosX = player.GetComponentInParent<Transform>().position.x;
        playerData.currentPosY = player.GetComponentInParent<Transform>().position.y;

        return playerData;
    }
    //Get all the enemies states and pos
    public List<EnemyData> GetEnemies()
    {
        List<EnemyData> enemyData = new List<EnemyData>();

        for (int i = 0; i < enemies.Length; i++)
        {
            //start recording enemy states
            enemyData.Add(new EnemyData());
            if (enemies[i] == null)
                enemyData[i].isDead = true; //check if the the enemy is destroyed already
            else
            {
                //record the enemy state and postion
                enemyData[i].isDead = false;
                enemyData[i].currentPosX = enemies[i].transform.localPosition.x;
                enemyData[i].currentPosY = enemies[i].transform.localPosition.y;
            }
        }

        //return all the enemies states and postionn in scene
        return enemyData;
    }
    //Load the previous state of the enemies in scene
    public void LoadEnemies(List<EnemyData> enemyData)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            //if the enemy has been recorded dead destroy the enemy
            if (enemyData[i].isDead)
                Destroy(enemies[i]);
            else
            {
                //give the recorded postion to the enemy
                enemies[i].transform.localPosition = new Vector3(enemyData[i].currentPosX, enemyData[i].currentPosY, 0);
            }
        }
    }
    //All the data that must be loaded for the scene from savedData located in SavingManager
    public abstract void Load();
    //Record all data that must be saved from scene and save it to savedData located in SavingManager and then to the JSON file
    public abstract void SaveToJSON(string nameSaveFile);
    //Just save to savedData (for going between scenes without saving to JSON)
    public abstract void SaveToSavedData();
}
