using System.Collections.Generic;

public sealed class RatChest : BodyPart
{
    public RatChest(Body body) : base(body)
    {
        Hp = MaxHp;
    }

    public override int MaxHp => 10;
    public override float Hp { get; protected set; }
    public override float Size => 100;
    public override IEnumerable<Clothes> Clothes => null;
}