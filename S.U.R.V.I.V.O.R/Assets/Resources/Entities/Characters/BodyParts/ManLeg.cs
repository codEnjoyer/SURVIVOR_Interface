using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class ManLeg : BodyPart
{
    public Clothes Boots { get; set; }

    public Clothes Pants { get; set; }
    
    public ManLeg(Body body) : base(body)
    {
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
}