#if UNITY_EDITOR
using System;

namespace GoogleSheetLink.DataParsers
{
    public static class GunModuleDataParser
    {
        public static GunModuleData Parse(string[] param)
        {
            var deltaRecoil = float.Parse(param[0]);
            var deltaAccuracy = float.Parse(param[1]);
            var deltaNoise =float.Parse(param[2]);
            var deltaAverageDistance = float.Parse(param[3]);
            var deltaDamage = float.Parse(param[4]);
            var deltaErgonomics = float.Parse(param[5]);
            var moduleType = Enum.Parse<GunModuleType>(param[6]);

            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            return new GunModuleData(deltaRecoil, deltaAccuracy, deltaNoise, deltaAverageDistance, deltaDamage,
                deltaErgonomics, moduleType)
            {
                name = "GunModuleData"
            };
        }
    }
}
#endif
