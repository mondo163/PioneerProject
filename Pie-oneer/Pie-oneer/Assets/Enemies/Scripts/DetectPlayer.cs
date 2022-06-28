using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool IsPlayerDetected { get; private set; }

    private string playerTag;
    private Collider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerDetected = false;
        playerTag = "Player";

        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            IsPlayerDetected = true;
            //circleCollider.enabled = false;
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

