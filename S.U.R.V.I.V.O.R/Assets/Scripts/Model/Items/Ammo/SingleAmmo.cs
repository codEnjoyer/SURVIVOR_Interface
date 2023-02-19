using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Caliber", menuName = "Data/Caliber", order = 50)]
public class SingleAmmo: ScriptableObject
{
    [SerializeField] [Min(0)] private float keneeticDamage;//Какой процент урона точно пройдет по телу
    [SerializeField] [Min(0)] private float armorPenetratingChance;//Шанс пробить броню
    [SerializeField] [Min(0)] private float underArmorDamage;//Какой процент урона пройдет по телу при непробитии
    [SerializeField] [Min(0)] private float fullDamage;//Полный урон, наносится при отсутствии брони
    [SerializeField] [Min(0)] private float bleedingChance;//Шанс кровотечения, равен 0 при непробитии
    [SerializeField] [Min(0)] private float boneBrokingChance;//Шанс перелома кости, умножается на 1.3 при непробитии
    [SerializeField] [Min(0)] private float armorDamageOnPenetration;//Урон броне при непробитии
    [SerializeField] [Min(0)] private float armorDamageOnNonPenetration;//Урон броне при пробитии
    
    [SerializeField] [Min(0)] private float deltaOptimalFireDistanceBegin;//Изменение нижнего порога оптимальной дистанции
    [SerializeField] [Min(0)] private float deltaOptimalFireDistanceEnd;//Изменение верхнего порога оптимальной дистанции
    [SerializeField] [Min(0)] private float deltaSpreadSizeOnOptimalFireDistance;//Изменение круга разброса на оптимальной дистанции
    [SerializeField] private Caliber caliber;
    
    public SingleAmmo(float keneeticDamage, float armorPenetratingChance, float underArmorDamage, float fullDamage, float bleedingChance, float boneBrokingChance, float armorDamageOnPenetration, float armorDamageOnNonPenetration, float deltaOptimalFireDistanceBegin, float deltaOptimalFireDistanceEnd, float deltaSpreadSizeOnOptimalFireDistance, Caliber caliber)
    {
        this.keneeticDamage = keneeticDamage;
        this.armorPenetratingChance = armorPenetratingChance;
        this.underArmorDamage = underArmorDamage;
        this.fullDamage = fullDamage;
        this.bleedingChance = bleedingChance;
        this.boneBrokingChance = boneBrokingChance;
        this.armorDamageOnPenetration = armorDamageOnPenetration;
        this.armorDamageOnNonPenetration = armorDamageOnNonPenetration;
        this.deltaOptimalFireDistanceBegin = deltaOptimalFireDistanceBegin;
        this.deltaOptimalFireDistanceEnd = deltaOptimalFireDistanceEnd;
        this.deltaSpreadSizeOnOptimalFireDistance = deltaSpreadSizeOnOptimalFireDistance;
        this.caliber = caliber;
    }
    
    public Caliber Caliber => caliber;

    public float KeneeticDamage => keneeticDamage;

    public float ArmorPenetratingChance => armorPenetratingChance;

    public float UnderArmorDamage => underArmorDamage;

    public float FullDamage => fullDamage;

    public float BleedingChance => bleedingChance;

    public float BoneBrokingChance => boneBrokingChance;

    public float ArmorDamageOnPenetration => armorDamageOnPenetration;

    public float ArmorDamageOnNonPenetration => armorDamageOnNonPenetration;

    public float DeltaOptimalFireDistanceBegin => deltaOptimalFireDistanceBegin;

    public float DeltaOptimalFireDistanceEnd => deltaOptimalFireDistanceEnd;

    public float DeltaSpreadSizeOnOptimalFireDistance => deltaSpreadSizeOnOptimalFireDistance;
}
