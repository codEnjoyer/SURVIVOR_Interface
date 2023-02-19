using System.Collections.Generic;
using Model.ServiceClasses;
using UnityEngine;

namespace Model.GameEntity
{
    public interface IEntity
    {
        public void Attack(Vector3 targetDot);
    }
}