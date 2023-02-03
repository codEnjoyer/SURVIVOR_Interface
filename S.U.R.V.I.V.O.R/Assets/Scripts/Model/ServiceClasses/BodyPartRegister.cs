using System;
using Model.GameEntity;
using UnityEngine;

namespace Model.ServiceClasses
{
    [Serializable]
    public class BodyPartRegister
    {
        [field: SerializeField]
        public BodyPart BodyPart { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public int Significance { get; private set; } = 1;
    }
}