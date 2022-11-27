using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FightCharacter : MonoBehaviour
{
    public CharacterType Type;
    public int Energy;
    public int Health;
    public float Radius;
    public IGun RangeWeapon;
    public IMeleeWeapon MeleeWeapon;
    public bool Alive;
    public GameObject TargetToHit;

    public void MakeShoot(GameObject targetObj, string targetName)
    {
        transform.LookAt(targetObj.transform);
        //RangeWeapon.Shoot(gameObject, targetObj, "Head");
    }

    public void MakeHit()
    {
        transform.LookAt(TargetToHit.transform);
        //MeleeWeapon.Hit(TargetToHit);
    }
}
