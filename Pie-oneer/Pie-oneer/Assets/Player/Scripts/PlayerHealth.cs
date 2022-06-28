using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private static PlayerHealth ph;

    public int health;
    public int HeartContainers; //number of heart containers
    public int MaxHealth = 5;

    public Image[] Hearts; //array of heart containers player could have

    public Sprite FullContainer;
    public Sprite EmptyContainer;
    public AudioSource deathSound;
    public AudioSource tookDamage;
    private Animator animator;

    public bool dead;
    public bool invincible;

    void Start()
    {
        health = MaxHealth;
        HeartContainers = MaxHealth;
        animator = gameObject.GetComponent<Animator>();
        dead = false;
        invincible = false;
        ph = this;
    }
    public static PlayerHealth GetPlayerHealth()
    {
        return ph;
    }

    void Update()
    {
        updateHealthContainers();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Damage(1);
                //Debug.Log("lost health");
            }
        }
    }
    //applies weapon damage from an enemy weapon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("EnemyWeapon"))
            {
                IWeaponBehavior weapon = collision.gameObject.GetComponent<IWeaponBehavior>();
                if (weapon != null)
                {
                    Damage(weapon.ReturnWeaponDamage());
                }
            }
        }
    }
    public void updateHealthContainers()
    {
        
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < health)
            {
                Hearts[i].sprite = FullContainer; //makes full container
            }
            else
            {
                Hearts[i].sprite = EmptyContainer; //makes empty container
            }
            if (i < HeartContainers)
            {
                Hearts[i].enabled = true; //heart exists
            }
            else //heart does not exist
            {
                Hearts[i].enabled = false;
            }
        }

    }

    public void Damage(int value)
    {
        if(!invincible)
        {
            Debug.Log("Not Invicible");
            health = health - value; //take away health
            updateHealthContainers();
            tookDamage.Play(); //makes sound when taking damage

            if (health <= 0) //player dies
            {
                Death();
            }

            StartCoroutine(Invincibility()); //short invincibility phase
        }
        
    }

    public void Death()
    {
        dead = true;
        deathSound.Play();

        StartCoroutine(DeathAnimation());

    }

    IEnumerator Invincibility() //after player takes damage wait 3 seconds before player can be attacked again
    {
        invincible = true;
        Debug.Log("Invicible");
        yield return new WaitForSeconds(1);
        invincible = false;
    }

    IEnumerator DeathAnimation()
    {
        animator.SetBool("Dead", true); //play animation
        yield return new WaitForSeconds(6); 
        SceneManager.LoadScene("Death Menu");
    }

    public void Heal(int value)
    {
        health += value;
        updateHealthContainers();
        //if this goes past full then just set to full
        if (health > HeartContainers)
        {
            health = HeartContainers;
        }
    }
}
