using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float speed;
    public float attackingDistance;
    public GameObject detectionRadius;
    public GameObject bloodEffect;
    public Transform player;
    public Animator animator;
    public List<ItemToDrop> itemsToDrop = new List<ItemToDrop>();

    // TODO: Abstract weapon details to account for being attacked with any weapon
    public RustyStartingSwordBehavior playerWeapon;

    private DetectPlayer detectPlayer;
    private IEnemyState currentState;
    private bool takingDamage;

    void Start()
    {
        // Every enemy needs to have an idle state, as that's the starting state for the enemy state machine
        currentState = new Idle(this);
        detectPlayer = detectionRadius.GetComponent<DetectPlayer>();
        animator = gameObject.GetComponent<Animator>();
        takingDamage = false;

        //Get the transform from the Player
        player = DoNOTDestroyConstant.Instance.gameObject.transform.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentState.StateUpdate();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.StateExit();
            currentState = newState;
            currentState.StateEntered();
        }
    }

    public IEnemyState GetState()
    {
        return currentState;
    }

    public bool IsPlayerDetected()
    {
        return detectPlayer.IsPlayerDetected;
    }

    private void DropItems()
    {
        float dropChance;

        if (itemsToDrop.Count > 0)
        {
            foreach (ItemToDrop item in itemsToDrop)
            {
                dropChance = UnityEngine.Random.Range(1, 100);

                if (dropChance < item.dropRate)
                {
                    for (int i = 0; i < item.amount; i++)
                    {
                        Instantiate(item.item.transform, new Vector3(gameObject.transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f),
                                             gameObject.transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f),
                                             0), Quaternion.identity);
                    }
                }
            }
        }
    }

    // Handle when the enemy is killed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (!takingDamage)
            {
                takingDamage = true;
                health -= playerWeapon.damageGiven;
                Instantiate(bloodEffect, transform.position, Quaternion.identity);

                if (health <= 0)
                {
                    Destroy(gameObject);
                    DropItems();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            takingDamage = false;
        }
    }
}

[Serializable]
public struct ItemToDrop {
    public int amount;
    public int dropRate;
    public GameObject item;
}