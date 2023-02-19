using System;
using UnityEngine;

[Serializable]
public sealed class DamageInfo
{
    [SerializeField] [Min(0)] private float keneeticDamage; //Какой процент урона точно пройдет по телу
    [SerializeField] [Min(0)] private float armorPenetratingChance; //Шанс пробить броню
    [SerializeField] [Min(0)] private float underArmorDamage; //Какой процент урона пройдет по телу при непробитии
    [SerializeField] [Min(0)] private float onArmorPenetrationDamage; //Какой процент урона пройдет по телу при пробитии
    [SerializeField] [Min(0)] private float fullDamage; //Полный урон, наносится при отсутствии брони
    [SerializeField] [Min(0)] private float bleedingChance; //Шанс кровотечения, равен 0 при непробитии
    [SerializeField] [Min(0)] private float boneBrokingChance; //Шанс перелома кости, умножается на 1.3 при непробитии
    [SerializeField] [Min(0)] private float armorDamageOnPenetration; //Урон броне при непробитии
    [SerializeField] [Min(0)] private float armorDamageOnNonPenetration; //Урон броне при пробитии

    private float randomCoefficientOfDamage;

    public float KeneeticDamage => keneeticDamage;

    public float OnArmorPenetrationDamage => onArmorPenetrationDamage;

    public float RandomCoefficientOfDamage => randomCoefficientOfDamage;

    public float ArmorPenetratingChance => armorPenetratingChance;

    public float UnderArmorDamage => underArmorDamage;

    public float FullDamage => fullDamage;

    public float BleedingChance => bleedingChance;

    public float BoneBrokingChance => boneBrokingChance;

    public float ArmorDamageOnPenetration => armorDamageOnPenetration;

    public float ArmorDamageOnNonPenetration => armorDamageOnNonPenetration;

    public DamageInfo(float keneeticDamage, float armorPenetratingChance, float underArmorDamage,
        float onArmorPenetrationDamage, float fullDamage, float bleedingChance, float boneBrokingChance,
        float armorDamageOnPenetration, float armorDamageOnNonPenetration, float randomCoefficientOfDamage)
    {
        this.keneeticDamage = keneeticDamage;
        this.armorPenetratingChance = armorPenetratingChance;
        this.underArmorDamage = underArmorDamage;
        this.onArmorPenetrationDamage = onArmorPenetrationDamage;
        this.fullDamage = fullDamage;
        this.bleedingChance = bleedingChance;
        this.boneBrokingChance = boneBrokingChance;
        this.armorDamageOnPenetration = armorDamageOnPenetration;
        this.armorDamageOnNonPenetration = armorDamageOnNonPenetration;
        this.randomCoefficientOfDamage = randomCoefficientOfDamage;
    }

    public DamageInfo(float damage)
    {
        this.keneeticDamage = damage;
        this.armorPenetratingChance = 0;
        this.underArmorDamage = 0;
        this.fullDamage = 0;
        this.bleedingChance = 0;
        this.boneBrokingChance = 0;
        this.armorDamageOnPenetration = 0;
        this.armorDamageOnNonPenetration = 0;
    }
}