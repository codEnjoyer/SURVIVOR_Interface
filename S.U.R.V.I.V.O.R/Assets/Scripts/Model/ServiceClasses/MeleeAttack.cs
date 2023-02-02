using System;
using UnityEngine;

namespace Model.ServiceClasses
{
    [Serializable]
    public class MeleeAttack
    {
        [field: SerializeField] public DamageInfo  DamageInfo { get; private set; }
        [field: SerializeField] public float Distance { get; private set; }
    }
}