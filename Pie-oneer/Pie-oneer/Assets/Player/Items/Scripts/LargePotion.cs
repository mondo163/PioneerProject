using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePotion : MonoBehaviour, IItem
{
    public string Name
    {
        get { return "Large Health Potion"; }
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
        // Add even more health to player
    }
}
