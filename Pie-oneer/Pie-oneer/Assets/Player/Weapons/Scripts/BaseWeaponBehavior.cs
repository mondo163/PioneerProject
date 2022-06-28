using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponBehavior : MonoBehaviour
{
    #region Rotate Weapon
    public float maxTurnSpeed = 280;
    public float smoothTime = 0.3f;
    protected float angle;
    protected float currentVelocity;
    #endregion

    protected virtual void RotateWeapon()
    {
        //code from https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        //Had a problem where the sword was 90 degrees ahead of mouse
        float targetAngle = Vector2.SignedAngle(new Vector2(1, 0), direction) - 90; 

        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
