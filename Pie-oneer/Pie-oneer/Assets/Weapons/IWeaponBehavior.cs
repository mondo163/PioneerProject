using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This interface can be used to abstractly take the weapon damage and apply it to the characters
public interface IWeaponBehavior 
{
    //function that returns weapon damage
    int ReturnWeaponDamage();
}
