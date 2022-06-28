using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueMiniBossAI : MonoBehaviour
{
    public int health;
    public float timeBetweenShots;
    public GameObject detectionRadius;
    public Transform player;
    public List<ItemToDrop> itemsToDrop = new List<ItemToDrop>();
    public Animator animator;
    public AudioSource transformingSFX;
    public Transform fireTrans;
    public FireballWeapon fireball;

    private DetectPlayerStatue detectPlayer;
    private IEnemyState currentState;
    private bool takingDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = new StatueIdle(this);
        detectPlayer = detectionRadius.GetComponent<DetectPlayerStatue>();
        takingDamage = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (!takingDamage)
            {
                takingDamage = true;
                health -= collision.gameObject.GetComponent<IWeaponBehavior>().ReturnWeaponDamage(); 
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
    public void DropItems()
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
    [Serializable]
    public struct ItemToDrop
    {
        public int amount;
        public int dropRate;
        public GameObject item;
    }
}
