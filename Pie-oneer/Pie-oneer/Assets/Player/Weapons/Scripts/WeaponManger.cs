using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : MonoBehaviour
{
    public GameObject[] weapons;
    private GameObject currentWeapon;
    private float playerSizeX;
    private int weaponIndexCurrent;
    // Start is called before the first frame update
    void Start()
    {
        playerSizeX = gameObject.GetComponentInParent<BoxCollider2D>().size.x;
        weaponIndexCurrent = 0;

        currentWeapon = weapons[weaponIndexCurrent];
        currentWeapon = Instantiate(currentWeapon, gameObject.transform.position + currentWeapon.transform.localPosition, Quaternion.identity, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if(!(++weaponIndexCurrent < weapons.Length))
                weaponIndexCurrent = 0;

            Destroy(currentWeapon);
            currentWeapon = weapons[weaponIndexCurrent];

            Vector3 tempPosition = gameObject.transform.position + currentWeapon.transform.localPosition;

            currentWeapon = Instantiate(currentWeapon, tempPosition, Quaternion.identity, gameObject.transform);
        }
        if(PlayerHealth.GetPlayerHealth().dead)
        {
            Destroy(currentWeapon);
        }
    }
}
