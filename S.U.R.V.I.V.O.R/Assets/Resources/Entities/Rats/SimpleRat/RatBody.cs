using System.Collections.Generic;
using Model.GameEntity;

public class RatBody : Body
{
    public RatChest Chest { get; private set; }

    public RatBody()
    {
        Chest = new RatChest(this);
        bodyParts.Add(Chest);
        MaxCriticalLoses = bodyParts.Count;
    }
}