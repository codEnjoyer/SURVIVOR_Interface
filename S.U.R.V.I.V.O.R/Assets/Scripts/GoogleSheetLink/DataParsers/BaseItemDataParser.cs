#if UNITY_EDITOR
namespace GoogleSheetLink.DataParsers
{
    public static class BaseItemDataParser
    {
        public static BaseItemData Parse(string[] param)
        {
            var name = param[0];
            var description = param[1];
            var size = SizeParser.Parse(param[2]);
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
#endif
