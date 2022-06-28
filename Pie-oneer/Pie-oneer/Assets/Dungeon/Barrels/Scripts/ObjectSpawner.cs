using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    public SpawnObjectProbality[] objectsThatCanSpawn;

    //Return spawn object based off of range 
    public GameObject ReturnSpawnObject()
    {
        int randomNum = Random.Range(1, 101);

        GameObject spawnObject = null;

        foreach (SpawnObjectProbality obj in objectsThatCanSpawn)
        {
            if (obj.start <= randomNum && randomNum <= obj.end)
            {
                spawnObject = obj.spawnObject;
                break;
            }
        }

        //if it returns null then no object is spawned
        return spawnObject;
    }

    public IEnumerator SpawnObject(Vector3 vector, Quaternion quaternion, float timeUnilSpawn)
    {
        yield return new WaitForSeconds(timeUnilSpawn);
        //Spawn object
        GameObject gameObjectToSpawn = ReturnSpawnObject();
        if (gameObjectToSpawn != null)
            Instantiate(gameObjectToSpawn, vector, quaternion);
    }

}

[System.Serializable]
public struct SpawnObjectProbality
{
    [Header("Spawn Probality via Range of 100 (ex: '1-25' results in 25%)")]
    public int start;
    public int end;
    public GameObject spawnObject;
}
