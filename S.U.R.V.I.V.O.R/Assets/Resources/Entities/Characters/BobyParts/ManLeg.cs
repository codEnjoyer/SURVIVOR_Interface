using System.Collections.Generic;

public class ManLeg : BodyPart
{
    public Clothes Pants { get; set; }

    public Clothes Boots { get; set; }

    public ManLeg(Body body) : base(body)
    {
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
    public override IEnumerable<Clothes> Clothes { get; }
}