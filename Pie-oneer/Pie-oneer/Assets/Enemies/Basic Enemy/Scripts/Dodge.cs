using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : IEnemyState
{
    private EnemyAI enemy;
    private float dodgeTimer;
    private float elaspedTime;
    private Transform playerWeapon;

    public Dodge(EnemyAI enemy)
    {
        this.enemy = enemy;

        // Set dodge timer to last 0.3 seconds
        dodgeTimer = 0.3f;
        elaspedTime = 0;
        GameObject pcWeapon = GameObject.FindGameObjectWithTag("Weapon");
        playerWeapon = pcWeapon != null ? pcWeapon.GetComponent<Transform>() : null;
    }

    public void StateEntered()
    { }

    public void StateExit()
    { }

    public void StateUpdate()
    {
        if(playerWeapon == null)
            return;

        elaspedTime += Time.deltaTime;

        Vector3 direction = playerWeapon.position - enemy.transform.position;
        Vector3 newPosition = -direction.normalized * 1.5f;

        // Move enemy back opposite of the direction of the player's sword
        // enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, newPosition, 5 * Time.deltaTime);

        // Decide to move left, right, up, or down based on where the player's sword is pointing
        // TODO: Make sure only one of these can trigger per dodge
        if (playerWeapon.rotation.z > 0 && playerWeapon.rotation.z < 90)
        {
            enemy.transform.Translate(Vector2.left * Time.deltaTime * 7.5f, playerWeapon);
        }
        else if (playerWeapon.rotation.z > 90 && playerWeapon.rotation.z < 180)
        {
            enemy.transform.Translate(Vector2.down * Time.deltaTime * 7.5f, playerWeapon);
        }
        else if (playerWeapon.rotation.z < 180 && playerWeapon.rotation.z > -90)
        {
            enemy.transform.Translate(Vector2.right * Time.deltaTime * 7.5f, playerWeapon);
        }
        else if (playerWeapon.rotation.z < -90 && playerWeapon.rotation.z < 0)
        {
            enemy.transform.Translate(Vector2.up * Time.deltaTime * 7.5f, playerWeapon);
        }

        if (elaspedTime >= dodgeTimer)
        {
            // Dodge has finished
            enemy.ChangeState(new Move(enemy));
        }
    }
}
