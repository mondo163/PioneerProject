using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : MonoBehaviour 
{
    //Singleton Pattern
    public static SavingManager Instance { get; private set; }
    public bool isLoaded;
    public SaveData savedData;
    public bool isPlayerLoaded;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete ALL "Dont destroy" .
        if (Instance != null && Instance != this)
        {
            DontDestroyOnLoadManager.DestroyAll();
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoadManager.dontDestroyOnLoadObjects.Add(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoadManager.dontDestroyOnLoadObjects.Add(gameObject);
        }
    }

    private void Start()
    {
        // LoadFromJSON();
        //isLoaded = false;
        //savedData = null;
    }

    private void Update()
    {

        if(!isLoaded && PlayerHealth.GetPlayerHealth() != null)
        {
            isLoaded = true;
        }
    }

    public void SaveToJSON(string name)
    {
        string json = JsonUtility.ToJson(savedData, true);

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Saves/" + name + ".json", json);
    }

    public void LoadFromJSON(string name)
    {
        string json = File.ReadAllText(Application.dataPath + "/StreamingAssets/Saves/" + name + ".json");
        savedData = JsonUtility.FromJson<SaveData>(json);
    }
}
