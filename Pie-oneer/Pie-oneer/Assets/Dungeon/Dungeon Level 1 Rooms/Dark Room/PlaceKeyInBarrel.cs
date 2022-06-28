using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceKeyInBarrel : MonoBehaviour
{
    public List<GameObject> barrels;
    public GameObject key;
    public DoorBehaviour door;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectRandomBarrel());
    }

    private IEnumerator SelectRandomBarrel()
    {
        int random = Random.Range(0, barrels.Count);

        //set Barrel as special spawn
        barrels[random].GetComponent<BarrelBehaviour>().isSpecialSpawn = true;
        //connect door to key
        key.GetComponentInChildren<Key>().door = door;
        //place key in barrel
        barrels[random].GetComponent<BarrelBehaviour>().specialSpawn = key;

        //wait then delete gameobject
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
