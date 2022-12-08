using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class ManArm : BodyPart
{
    public ManArm(Body body) : base(body)
    {
        MaxHp = 100;
        Hp = MaxHp;
        Size = 100;
    }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    public override float Size { get; }
}