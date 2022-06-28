using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Add sounds affects for travelling  and destruction
public class UltrafireballBulletBehavior : MonoBehaviour, IWeaponBehavior
{
    public float speed = 20f;
    public Rigidbody2D ultraFireball;
    public AudioSource ultraFireballSFX;
    public int DAMAGE = 1;
    // Start is called before the first frame update
    private void Start()
    {
        ultraFireball.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.name == "Walls" || hitInfo.gameObject.name == "OuterWall")
        {
            Destroy(this.ultraFireball.gameObject);
        }
        else if (hitInfo.name == "Player")
        {
            StartCoroutine(FireballHitPlayer());
        }

    }
    //Since these fireballs start stationary, you call the move function to get them going
    public void Move()
    {
        ultraFireball.velocity = transform.right * speed;
    }
    //plays the object sound and waits before destroying the object
    private IEnumerator FireballHitPlayer()
    {
        ultraFireballSFX.Play();
        yield return new WaitForSeconds(.25f);
        Destroy(this.ultraFireball.gameObject);
    }
    //returns weapon damage
    public int ReturnWeaponDamage()
    {
        return DAMAGE;
    }
}
