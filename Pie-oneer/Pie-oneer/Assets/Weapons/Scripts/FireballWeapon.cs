using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPrefab;
    public float betweenShotsPauseLength; //length of time in between each shot

    private Quaternion fireDefaultStartRot;
    private int ROTATION_DEGREES = 30;
    private void Start()
    {
        fireDefaultStartRot = firePoint.rotation;
    }
    //function to use fireballs closckwise using a 
    public IEnumerator ShootCounterClockwise()
    {
        //creates a fireball and shoots, then rotates 30 degrees
        for (int i = 0; i < 360; i+=ROTATION_DEGREES)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, (i-90)); //-90 to start facing down
            
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(betweenShotsPauseLength);
        }      
    }
    //shoots fireballs clockwise one at a time
    public IEnumerator ShootClockwise()
    {
        //creates a fireball and shoots, then rotates 30 degrees
        for (int i = 0; i > -360; i -= ROTATION_DEGREES)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, (i-90)); //-90 to start facing down
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(betweenShotsPauseLength);
        }
    }
    //shoots fireballs two at a time in opposites directions
    public IEnumerator ShootSimultaneously()
    {
        //initial values
        int totalFireballs = 360 / 30;
        int RotationStartingTop = 90;
        int RotationStartingBottom = -90;

        for (int i = 0; i < totalFireballs; i++)
        {
            //first fireball
            firePoint.rotation = Quaternion.Euler(0, 0, RotationStartingTop);
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            //second fireball
            firePoint.rotation = Quaternion.Euler(0, 0, RotationStartingBottom);
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(betweenShotsPauseLength);

            RotationStartingTop += ROTATION_DEGREES;
            RotationStartingBottom += ROTATION_DEGREES;
        }

        ResetFirepoint();
    }

    //shoots single fireball in direction of player
    public void ShootSinglesAtPlayer(float PlayerAngle)
    {
        firePoint.rotation = Quaternion.Euler(0, 0, PlayerAngle);
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        ResetFirepoint();
    }

    //resets position of firepoint from stored default
    private void ResetFirepoint()
    {
        firePoint.rotation = fireDefaultStartRot;
    }
}
