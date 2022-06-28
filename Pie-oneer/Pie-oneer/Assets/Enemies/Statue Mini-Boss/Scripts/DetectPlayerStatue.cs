using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerStatue : MonoBehaviour
{
    public bool IsPlayerDetected { get; private set; }

    private string playerTag;
    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerDetected = false;
        playerTag = "Player";

        collider2D = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            IsPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            IsPlayerDetected = false;
        }
    }
}

