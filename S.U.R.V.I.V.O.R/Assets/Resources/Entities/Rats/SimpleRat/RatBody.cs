using System.Collections.Generic;

public class RatBody : Body
{
    public RatChest Chest { get; private set; }

    public RatBody()
    {
        Chest = new RatChest(this);
        bodyParts.Add(Chest);
        MaxCriticalLoses = 1;
    }
}