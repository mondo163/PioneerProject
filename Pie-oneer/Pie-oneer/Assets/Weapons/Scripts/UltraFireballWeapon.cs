using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UltraFireballWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject uFireballPrefab;
    public AudioSource firingSFX;

    private Vector3 fireDefaultStartPos;
    private Quaternion fireDefaultStartRot;
    private int ROTATION_DEGREES = 30;
    private float CIRCLE_RADIUS = 1.8f;
    private GameObject[] fireballs = new GameObject[12];

    private void Start()
    {
        fireDefaultStartPos = firePoint.position;
        fireDefaultStartRot = firePoint.rotation;
    }
    //shoots behaviour for the ultra fireballs. It sets 12 fireballs, pauses for the allotted sleeptime
    //and then fires the 12 fireballs at the player.
    //set as a coroutine to make sure that pauses are happening
    public IEnumerator ShootUltras()
    {
        
        //initial values for the placement of the fireballs
        Vector3 startingPosition = firePoint.position;
        float rotation = -90f;
        float xAxis = 0;
        float yAxis = (startingPosition.y - CIRCLE_RADIUS);
        
        //create fireballs and add objects to a list. 
        for (int i = 0; i < 12; i++)
        {
            //sets fireballs and adds them to a list
            firePoint.rotation = Quaternion.Euler(0, 0, rotation);
            firePoint.position = new Vector3(xAxis, yAxis, 0);
            fireballs[i] = (GameObject)Instantiate(uFireballPrefab, firePoint.position, firePoint.rotation);
            
            //creates new position points in circle for the next fireball
            rotation += ROTATION_DEGREES;
            xAxis = CIRCLE_RADIUS * Mathf.Cos(rotation * Mathf.Deg2Rad);
            yAxis = (CIRCLE_RADIUS * Mathf.Sin(rotation * Mathf.Deg2Rad)) + startingPosition.y;

            //waits before creating the next fireball
            yield return new WaitForSeconds(.25f);
        }

        //pause for entered seconds before firing
        yield return new WaitForSeconds(.75f);

        //fire objects
        firingSFX.Play();
        for (int i = 0; i < 12; i++)
        {
            if (fireballs[i] != null)
                fireballs[i].GetComponent<UltrafireballBulletBehavior>().Move();
        }

        //pause for the fireballs to reach the outer walls. 
        yield return new WaitForSeconds(2f);


        //resets the position of the firepoint
        ResetFirepoint();
    }
    //manually destroy the ultrafireball game objects
    //needed since stopcoroutine will not destroy them automatically
    public void DestroyObjects()
    {
        //resets position of firepoint 
        ResetFirepoint();
        if (fireballs.Length != 0 || fireballs != null)
        {
            foreach (var item in fireballs)
            {
                Destroy(item.gameObject);
            }
        }
        
    }
    //resets the position of the firepoint from the stored defaults
    private void ResetFirepoint()
    {
        firePoint.position = fireDefaultStartPos;
        firePoint.rotation = fireDefaultStartRot;
    }
}
