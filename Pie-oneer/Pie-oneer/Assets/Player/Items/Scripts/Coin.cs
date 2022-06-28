using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public string Name
    {
        get { return "Coin"; }
    }

    public Sprite Sprite
    {
        get { return spriteRenderer.sprite; }
    }

    public void Collect()
    {
        StartCoroutine(PlaySoundAndDestroy());
    }

    public void Use()
    { }

    IEnumerator PlaySoundAndDestroy()
    {
        // Sound effect needs to finish playing before the coin can be destroyed.
        // Disable sprite and collider to fake coin disappearing.
        gameObject.GetComponent<AudioSource>().Play();
        spriteRenderer.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}

public class CoinEventArgs : EventArgs
{
    public CoinEventArgs(int coinBalance)
    {
        CoinBalance = coinBalance;
    }

    public int CoinBalance;
}

