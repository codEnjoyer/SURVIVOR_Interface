using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    public abstract float Initiative { get; }
    public abstract int SpeedInFightScene { get; }
    public abstract Body Body { get; }
    public abstract void Attack(List<BodyPart> targets, float distance);
}