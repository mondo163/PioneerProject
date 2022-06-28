using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Add sounds affects for travelling  and destruction
public class FireballBulletBehavior : MonoBehaviour, IWeaponBehavior
{ 
    public float speed = 20f;
    public Rigidbody2D fireball;
    public AudioSource fireballHitSFX;
    public AudioSource fireballFiringSFX;
    public int DAMAGE = 1;

    // Start is called before the first frame update
    private void Start()
    {
        fireballFiringSFX.Play();
        fireball.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.name == "Walls" || hitInfo.gameObject.name == "OuterWall")
        {
            Destroy(this.fireball.gameObject);
        }
        else if (hitInfo.name == "Player")
        {
            StartCoroutine(FireballHitPlayer());
        }
    
    }

    private IEnumerator FireballHitPlayer()
    {
        fireballHitSFX.Play();
        yield return new WaitForSeconds(.25f);
        Destroy(this.fireball.gameObject);
    }
    //returns weapon damage
    public int ReturnWeaponDamage()
    {
        return DAMAGE;
    }
}
