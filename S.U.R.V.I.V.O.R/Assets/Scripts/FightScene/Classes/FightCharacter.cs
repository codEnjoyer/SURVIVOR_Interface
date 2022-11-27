using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FightCharacter : MonoBehaviour
{
    public Entity Target { get; private set; }
    public CharacterType Type { get; private set; }

    public FightCharacter(Entity target, CharacterType type)
    {
        Target = target;
        Type = type;
    }

    public int Energy => Target.SpeedInFightScene;
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
        Target.Attack(new List<BodyPart>(), 10);
    }

    public void OnEnable()
    {
        Debug.Log(Target);
        Debug.Log(Target.Body);
        Target.Body.Died += OnDied;
    }

    public void OnDisable()
    {
        Target.Body.Died -= OnDied;
    }

    private void OnDied()
    {
        FightSceneController.Instance.DeleteDeathCharacterFromQueue(this);
        Destroy(gameObject);
    }
}
