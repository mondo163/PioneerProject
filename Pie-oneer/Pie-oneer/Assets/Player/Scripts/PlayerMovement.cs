using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Inventory inventory;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    // good for processing inputs
    void Update()
    {
        if (PlayerHealth.GetPlayerHealth().dead == false)
        {
            ProcessInputs();

            //If player is moving left flip sprite so player character looks like they are running left
            if (moveDirection.x < 0)
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            else if (moveDirection.x > 0)
                gameObject.transform.localScale = new Vector3(1, 1, 1);

            //if player is moving play footstep sound
            if (rb.velocity.magnitude != 0)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
                audioSource.Stop();
        }
    }

    //good for physics calculations
    private void FixedUpdate()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying && !PlayerHealth.GetPlayerHealth().dead)
            Move();
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed* moveDirection.x, moveSpeed*moveDirection.y);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        //normalized returns vector with mag of 1
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Make sure the collider is only colliding with the player and not any of its children.
        if (collider.IsTouching(gameObject.transform.GetComponent<Collider2D>()))
        {
            IItem item = collider.GetComponent<IItem>();

            if (item != null)
            {
                item.Collect();
                inventory.AddItem(item);
            }
        }
    }
}
