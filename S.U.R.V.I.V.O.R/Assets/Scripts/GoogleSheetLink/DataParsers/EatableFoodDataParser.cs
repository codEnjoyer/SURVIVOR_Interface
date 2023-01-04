using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleSheetLink.DataParsers
{
    public class EatableFoodDataParser
    {
        public static EatableFoodData Parse(string[] param)
        {
            var deltaWater = int.Parse(param[0]);
            var deltaHunger = int.Parse(param[1]);
            var deltaEnergy = int.Parse(param[2]);
            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            return new EatableFoodData(deltaWater,deltaHunger, deltaEnergy)
            {
                name = "EatableFoodData"
            };
        }
    }
}