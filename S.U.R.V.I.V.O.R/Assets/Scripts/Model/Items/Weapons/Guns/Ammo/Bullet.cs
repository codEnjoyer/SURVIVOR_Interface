using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bullet", menuName = "Data/Bullet Data", order = 50)]
public class Bullet : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float extraFireDistance;
    [SerializeField] private float armorPenetrating;
    [SerializeField] private float boneBreakingChance;
    [SerializeField] private float bleedingChance;
    [SerializeField] private float recoil;
    [SerializeField] private float extraAccuracy;
    [SerializeField] private float noise;

    public float Damage => damage;
    public float ExtraFireDistance => extraFireDistance;
    public float ArmorPenetrating => armorPenetrating;
    public float BoneBreakingChance => boneBreakingChance;
    public float BleedingChance => bleedingChance;
    public float Recoil => recoil;
    public float ExtraAccuracy => extraAccuracy;
    public float Noise => noise;
}