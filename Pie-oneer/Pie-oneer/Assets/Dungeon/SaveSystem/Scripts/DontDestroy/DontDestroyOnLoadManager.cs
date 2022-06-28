using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DontDestroyOnLoadManager
{
    public static List<GameObject> dontDestroyOnLoadObjects = new List<GameObject>();

    //Destroy all objects in list
    //Is used for going back to Main Menu or Death Scene
    public static void DestroyAll()
    {
        foreach (var gameObject in dontDestroyOnLoadObjects)
        {
            if (gameObject != null)
                UnityEngine.Object.Destroy(gameObject);
        }

        dontDestroyOnLoadObjects.Clear();
    }
}
