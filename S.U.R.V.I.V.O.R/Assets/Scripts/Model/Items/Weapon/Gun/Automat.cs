using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Automat : Gun
{
    [SerializeField] private GunData data;

    public override GunData Data => data;

    public override void Attack(Vector3 targetPoint, Skills skills)
    {
        var distance = Vector3.Distance(transform.position, targetPoint);

        //var ammo = CurrentMagazine.GetAmmo(); //Извлекаем патрон
        //if (ammo == null)
            //return; //TODO Осечка

            var currentOptDistBegin = OptimalFireDistanceBegin;// + ammo.DeltaOptimalFireDistanceBegin;
            var currentOptDistEnd = OptimalFireDistanceEnd;// + ammo.DeltaOptimalFireDistanceEnd;

        var distanceDifference =
            distance >= currentOptDistBegin
            && distance <= currentOptDistEnd
                ? 1
                : distance <= currentOptDistBegin
                    ? distance /
                      currentOptDistBegin //TODO сделать "экспоненциальное" увеличение с помощью коэффициента в степени
                    : currentOptDistEnd / distance; //TODO сделать линейное увеличение, как у конуса

        var spreadSize =
            SpreadSizeOnOptimalFireDistance;// + ammo.DeltaSpreadSizeOnOptimalFireDistance; //Чем меньше точность, тем меньше круг
        var maxRo = spreadSize * (1 + (1 - distanceDifference));
        var shotedDot = GetShotеedDot(targetPoint, maxRo, skills);

        var position = transform.position;
        var wasHitted = Physics.Raycast(position, shotedDot - position, out var hit);
        var target = hit.transform.gameObject.GetComponent<BodyPart>();
        if (wasHitted && target != null)
        {
            target.TakeDamage(GetShot()); //ammo));
        }

        //var sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //sphere1.transform.position = newTarget;
        //}
    }

    private Vector3 GetShotеedDot(Vector3 targetPoint, float maxRo, Skills skills)
    {
        //for (var l = 0; l < 100; l++)
        //{
        var ro = rnd.NextDouble() * maxRo;
        var fi = rnd.NextDouble() * (2 * 3.14f);

        //Смещение в новых координатах
        var newX = (float)(ro * Math.Cos(fi));
        var newY = (float)(ro * Math.Sin(fi));
        var newZ = 0;

        //НАХОЖДЕНИЕ НОВОГО БАЗИСА
        var position = transform.position;
        var newK = targetPoint - position;
        var newI = new Vector3(0, -newK.z, newK.y);
        var newJ = Vector3.Cross(newI, newK);

        newK = newK.normalized;
        newI = newI.normalized;
        newJ = newJ.normalized;

        //Смещение в старых координатах
        var offsetInOldCoordinates = newI * newX + newJ * newY + newK * newZ;

        return offsetInOldCoordinates + targetPoint;
    }

    private DamageInfo GetShot()//SingleAmmo ammo)
    {
        //TODO Уменьшать урон пропорционально отдалению от верхней планки оптимальной дистанции

        // var keneeticDamage = ammo.KeneeticDamage; //Какой процент урона точно пройдет по телу
        // var armorPenetratingChance = ammo.ArmorPenetratingChance; //Шанс пробить броню
        // var underArmorDamage = ammo.UnderArmorDamage; //Какой процент урона пройдет по телу при непробитии
        // var fullDamage = ammo.FullDamage; //Полный урон, наносится при отсутствии брони
        // var bleedingChance = ammo.BleedingChance; //Шанс кровотечения, равен 0 при непробитии
        // var boneBrokingChance = ammo.BoneBrokingChance; //Шанс перелома кости, умножается на 1.3 при непробитии
        // var armorDamageOnPenetration = ammo.ArmorDamageOnPenetration; //Урон броне при непробитии
        // var armorDamageOnNonPenetration = ammo.ArmorDamageOnNonPenetration;

        //return new DamageInfo(keneeticDamage, armorPenetratingChance, underArmorDamage, fullDamage, bleedingChance,
        //    boneBrokingChance, armorDamageOnPenetration, armorDamageOnNonPenetration);
        return new DamageInfo(20,
            0.3f,
            0,
            1,
            0,
            0,
            0,
            0,
            0,
            1 + rnd.Next(-10, 10) / 100f);
    }
}