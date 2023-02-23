
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SemiAutomaticRifle : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;
    public override void Attack(Vector3 targetPoint, Skills skills)
    {
        //Радиус круга 
        var distance = Vector3.Distance(transform.position, targetPoint);
        var distanceDifference =  distance >= Data.OptimalFireDistanceBegin && distance <= Data.OptimalFireDistanceEnd  
            ? 0 
            : distance <= Data.OptimalFireDistanceBegin 
                ? distance/Data.OptimalFireDistanceBegin //TODO сделать "экспоненциальное" увеличение с помощью коэффициента в степени
                : Data.OptimalFireDistanceEnd/distance; //TODO сделать линейное увеличение, как у конуса
        var spreadSize = Data.SpreadSizeOnOptimalFireDistance + GunModules.Sum(x => x.Data.DeltaSpreadSizeOnOptimalFireDistance); //Чем меньше точность, тем меньше круг
        var maxRo = spreadSize * (1 + (1 - distanceDifference));
        var ro = (float)rnd.NextDouble() * maxRo;
        var fi = (float)rnd.NextDouble() * (2 * 3.14f);
        var (x, y) = PolarToCartesian(ro, fi);

        //НАХОЖДЕНИЕ НОВОГО БАЗИСА
            var newK = (targetPoint - transform.position).normalized;
            var newI = new Vector3(0, -newK.z, newK.y).normalized;
            var newJ = Vector3.Cross(newI,newK).normalized;
            
            var mainBasisPoint = newI * x + newJ * y;
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = mainBasisPoint;
        
        //
        
        (float x, float y) PolarToCartesian(float ro, float fi)
        {
            var _x = ro * Math.Cos( fi );
            var _y = ro * Math.Sin( fi );
            return ((float)_x,(float)_y);
        }
    }
}
