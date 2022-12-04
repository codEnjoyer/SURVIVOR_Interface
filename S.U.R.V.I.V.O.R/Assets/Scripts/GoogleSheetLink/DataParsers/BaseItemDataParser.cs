using System;
using System.Threading.Tasks;
using Extension;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace GoogleSheetLink.DataParsers
{
    public static class BaseItemDataParser
    {
        private static readonly SizeParser sizeParser = new();

        public static BaseItemData Parse(string[] param)
        {
            var name = param[0];
            var description = param[1];
            var size = sizeParser.Parse(param[2]);
            //var sprite = await SpriteLoader.LoadSprite(param[3]);
            var weight = float.Parse(param[4]);

            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            var baseItemData = new BaseItemData(name, description, size, null, weight)
            {
                name = "ItemData"
            };
            return baseItemData;
        }
    }
}