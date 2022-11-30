using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FightCharacter : MonoBehaviour
{
    public Entity Entity { get; private set; }
    public CharacterType Type { get; private set; }

    public void ApplyProperties(Entity target, CharacterType type, bool alive = true)
    {
        Entity = target;
        Type = type;
        Alive = alive;
    }

    public float Initiative => Entity.Initiative;
    public int Energy => Entity.SpeedInFightScene;
    public bool Alive = true;
    public GameObject TargetToHit;
    public readonly float radius;
    
    // public void MakeShoot(GameObject targetObj, string targetName)
    // {
    //     transform.LookAt(targetObj.transform);
    //     //RangeWeapon.Shoot(gameObject, targetObj, "Head");
    // }
    //
    // public void MakeHit()
    // {
    //     transform.LookAt(TargetToHit.transform);
    //     //MeleeWeapon.Hit(TargetToHit);
    // }

    public void Attack()
    {
        Entity.Attack(new List<BodyPart>(), 10);
    }

//     public void OnEnable()
//     {
//         Debug.Log(Target);
//         Debug.Log(Target.Body);
//         Target.Body.Died += OnDied;
//     }
//
//     public void OnDisable()
//     {
//         Target.Body.Died -= OnDied;
//     }
//
//     private void OnDied()
//     {
//         FightSceneController.Instance.DeleteDeathCharacterFromQueue(this);
//         Destroy(gameObject);
//     }
}
