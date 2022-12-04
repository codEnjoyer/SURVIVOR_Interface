using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GoogleSheetLink
{
    public class SpriteLoader
    {
        public static async Task<Sprite> LoadSprite(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;
            var texture = await GetRemoteTexture(url);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        private static async Task<Texture2D> GetRemoteTexture(string url)
        {
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                await Task.Delay(1000 / 30);
            }

            return www.result != UnityWebRequest.Result.Success ? null : DownloadHandlerTexture.GetContent(www);
        }
    }
}