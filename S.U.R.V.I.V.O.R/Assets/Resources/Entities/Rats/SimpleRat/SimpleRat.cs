using System.Collections.Generic;
using System.Linq;

public class SimpleRat : Entity
{
    public override Body Body => new RatBody();

    public override void Attack(ICollection<BodyPart> targets, float distance)
    {
        var target = targets.First();
        target.TakeDamage(new DamageInfo(10));
    }
}