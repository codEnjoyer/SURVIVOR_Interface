using System;
using Unity.VisualScripting.Antlr3.Runtime;

namespace GoogleSheetLink.DataParsers
{
    public static class ClothesDataParser
    {
        public static ClothData Parse(string[] param)
        {
            var maxArmor = int.Parse(param[0]);
            var inventorySize = SizeParser.Parse(param[1]);
            var warm = int.Parse(param[2]);
            var clothType = Enum.Parse<ClothType>(param[3], true);

            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            return new ClothData(maxArmor, inventorySize, warm, clothType)
            {
                name = "ClothData"
            };
        }
    }
}