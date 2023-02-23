using System;
using UnityEngine;

namespace Model.ServiceClasses
{
    [Serializable]
    public class MeleeAttack
    {
        [field: SerializeField] public float  Damage { get; private set; }
        [field: SerializeField] public float Distance { get; private set; }
    }
}