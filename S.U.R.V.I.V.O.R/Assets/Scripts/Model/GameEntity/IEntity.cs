using System.Collections.Generic;
using Model.ServiceClasses;

namespace Model.GameEntity
{
    public interface IEntity: IAlive
    {
        public void Attack(IEnumerable<AttackTarget> targets);
    }
}