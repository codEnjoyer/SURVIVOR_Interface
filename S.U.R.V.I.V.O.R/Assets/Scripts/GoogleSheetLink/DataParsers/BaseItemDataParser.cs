using System;
using System.Threading.Tasks;
using Extension;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace GoogleSheetLink.DataParsers
{
    public class BaseItemDataParser
    {
        private static readonly SizeParser sizeParser = new();

        public static async Task<BaseItemData> Parse(string[] param)
        {
            if (param.Length != 5)
                throw new ArgumentException(
                    $"При парсе {nameof(BaseItemData)} было переданно неправлильное колличество параметров!");
            var name = param[0];
            var description = param[1];
            var size = sizeParser.Parse(param[2]);
            var sprite = await SpriteLoader.LoadSprite(param[3]);
            // Sprite sprite = null; 
            var weight = float.Parse(param[4]);

            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            var baseItemData = new BaseItemData(name, description, size, sprite, weight)
            {
                name = "ItemData"
            };
            return baseItemData;
        }
    }
}