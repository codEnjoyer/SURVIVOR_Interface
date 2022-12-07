using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class ManStomach : BodyPart
{
    public Clothes Pants { get; set; }
    
    public ManStomach(Body body) : base(body)
    {
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
    public override IEnumerable<Clothes> Clothes { get; }
}