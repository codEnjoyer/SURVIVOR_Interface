using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace GoogleSheetLink
{
    public class BaseItemDataParser
    {
        private static readonly SizeParser sizeParser = new();

        public static BaseItemData Parse(string[] param)
        {
            if (param.Length != 5)
                throw new ArgumentException(
                    $"При парсе {nameof(BaseItemData)} было переданно неправлильное колличество параметров!");
            var name = param[0];
            var description = param[1];
            var size = sizeParser.Parse(param[2]);
            Sprite sprite = null;
            var weight = float.Parse(param[4]);
            
            // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
            var baseItemData = new BaseItemData(name, description, size, sprite, weight)
            {
                name = "ItemData"
            };
            return Object.Instantiate(baseItemData);
        }
        
        // private static async Task<Texture2D> GetRemoteTexture(string url)
        // {
        //     using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        //     var asyncOp = www.SendWebRequest();
        //     while (asyncOp.isDone == false)
        //     {
        //         await Task.Delay(100);
        //     }
        //     if (www.isDone) return DownloadHandlerTexture.GetContent(www);
        //     Debug.Log($"{www.error}, URL:{www.url}");
        //     return null;
        // }
    }
}