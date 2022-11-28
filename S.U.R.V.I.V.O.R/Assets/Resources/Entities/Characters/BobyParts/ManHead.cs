using System.Collections.Generic;

public sealed class ManHead : BodyPart
{
    public Clothes Hat;

    public ManHead(Body body) : base(body)
    {
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
    public override IEnumerable<Clothes> Clothes { get; }
}