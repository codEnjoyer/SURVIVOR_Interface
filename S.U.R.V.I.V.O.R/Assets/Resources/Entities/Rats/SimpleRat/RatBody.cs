using System.Collections.Generic;

public class RatBody : Body
{
    public readonly RatChest chest;
    public override ICollection<BodyPart> BodyParts { get; }
    protected override int CriticalLoses { get; }

    public RatBody()
    {
        chest = new RatChest(this);
        BodyParts = new List<BodyPart> {chest};
        CriticalLoses = 1;
    }
}