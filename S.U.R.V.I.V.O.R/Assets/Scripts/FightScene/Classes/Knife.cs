using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// public class Knife : MonoBehaviour, IMeleeWeapon
// {
//     public int Damage {get;set;}
//     public float Range{get;set;}

//     public Knife(int damage, float range)
//     {
//         Damage = damage;
//         Range = range;
//     }

//     public void Hit(GameObject targetObj)
//     {
//         var enemy = targetObj.GetComponent<Character>();
//         if (enemy == null)
//             throw new Exception("Не обнаружен компонент <Character>");

//         enemy.Health -= Damage;
//         Debug.Log(enemy.Health);

//         if (enemy.Health <= 0)
//         {
//             enemy.Alive = false;
//             Destroy(targetObj);
//             Debug.Log("Death");
//         }
//     }
// }
