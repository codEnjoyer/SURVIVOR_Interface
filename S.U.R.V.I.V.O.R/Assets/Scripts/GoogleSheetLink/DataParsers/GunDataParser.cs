using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoogleSheetLink.DataParsers
{
    public static class GunDataParser
    {
        public static GunData Parse(string[] param)
        {
            var fireRate = float.Parse(param[0]);
            var accuracy = float.Parse(param[1]);
            var extraDamage = float.Parse(param[2]);
            var fireDistance = float.Parse(param[3]);
            var ergonomics = float.Parse(param[4]);
            
            var caliber = Enum.Parse<Caliber>(param[5], true);
            var gunType = Enum.Parse<GunType>(param[6], true);
            var availableGunModules = param[7].Split(", ")
                .Select(x => Enum.Parse<GunModuleType>(x, true))
                .ToList();

            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            var gunData = new GunData(fireRate, accuracy, extraDamage, fireDistance, ergonomics, caliber, gunType,
                availableGunModules)
            {
                name = "GunData"
            };
            return gunData;
        }
    }
}