using System.Collections.Generic;
using Model.ServiceClasses;

namespace Model.GameEntity
{
    public interface IEntity
    {
        public void Attack(IEnumerable<AttackTarget> potentialTargets, out IEnumerable<IAlive> attackedTargets);
    }
}