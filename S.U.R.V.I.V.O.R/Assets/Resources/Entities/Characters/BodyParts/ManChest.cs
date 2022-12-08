using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class ManChest : BodyPart
{
    public Clothes Underwear { get; set; }

    public Clothes Jacket { get; set; }

    public Clothes Backpack { get; set; }
    public Clothes Vest { get; set; }

    public ManChest(Body body) : base(body)
    {
        MaxHp = 100;
        Hp = MaxHp;
        Size = 200;
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
}