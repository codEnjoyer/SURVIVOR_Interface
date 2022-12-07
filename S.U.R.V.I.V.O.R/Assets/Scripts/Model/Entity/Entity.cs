using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [SerializeField] private float initiative;
    [SerializeField] private int speedInFightScene;

    public float Initiative => initiative;
    public int SpeedInFightScene => speedInFightScene;

    public abstract Body Body { get; }
    public abstract void Attack(IEnumerable<BodyPart> targets, float distance);
}