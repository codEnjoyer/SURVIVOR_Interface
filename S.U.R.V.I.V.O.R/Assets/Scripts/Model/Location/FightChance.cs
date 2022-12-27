using System;
using UnityEngine;

[Serializable]
public class FightChance
{
    [SerializeField] private Fight fight;
    [SerializeField] [Min(1)] private int weightChance = 1;
    
    public Fight Fight => fight;
    public int WeightChance => weightChance;
}