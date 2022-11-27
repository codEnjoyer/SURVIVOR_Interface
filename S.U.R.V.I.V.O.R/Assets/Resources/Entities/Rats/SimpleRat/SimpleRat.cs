using System.Collections.Generic;
using System.Linq;

public class SimpleRat : Entity
{
    public override float Initiative => 15;
    public override int SpeedInFightScene => 12;
    public override Body Body => new RatBody();

    public override void Attack(List<BodyPart> targets, float distance)
    {
        var target = targets.First();
        target.TakeDamage(new DamageInfo(10));
    }
}