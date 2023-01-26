using System.Collections.Generic;
using System.Linq;
using Model.GameEntity;

namespace Model.Entities.Rats.SimpleRat
{
    public class SimpleRat : Entity
    {
        private RatBody body;

        public override Body Body => body;

        public override void Attack(IEnumerable<BodyPart> targets, float distance)
        {
            var target = targets.First();
            target.TakeDamage(new DamageInfo(40));
        }

        private void Awake()
        {
            body = new RatBody();
        }
    }
}