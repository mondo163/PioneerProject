using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IItem
{
    public string Name
    {
        get { return "Health Potion"; }
    }

    public Sprite Sprite
    {
        get { return gameObject.GetComponent<SpriteRenderer>().sprite; }
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

    public void Use()
    {
        // Add health to player
    }
}
