using System;
using UnityEngine;

[Serializable]
public class FightChance
{
    [SerializeField] private Fight fight;
    [SerializeField] private int weightChance = 1;
    
    public Fight Fight => fight;
    public int WeightChance => weightChance;
}