using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoogleSheetLink
{
    public class BaseItemDataParser
    {
        private readonly SizeParser sizeParser;
        public BaseItemDataParser()
        {
            sizeParser = new SizeParser();
        }

        public BaseItemData Parse(string[] param)
        {
            if (param.Length != 5)
                throw new ArgumentException(
                    $"При парсе {nameof(BaseItemData)} было переданно неправлильное колличество параметров!");
            var name = param[0];
            var description = param[1];
            var size = sizeParser.Parse(param[2]);
            Sprite sprite = null;
            var weight = float.Parse(param[4]);
            var baseItemData = new BaseItemData(name, description, size, sprite, weight);
            return Object.Instantiate(baseItemData);
        }
    }
}