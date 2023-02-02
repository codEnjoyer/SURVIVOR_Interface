using System;
using UnityEngine;

namespace Model.ServiceClasses
{
    [Serializable]
    public class MeleeAttack
    {
        [field: SerializeField] public DamageInfo DamageInfoForAttack { get; private set; }
        [field: SerializeField] public float DistanceForAttack { get; private set; }
    }
}