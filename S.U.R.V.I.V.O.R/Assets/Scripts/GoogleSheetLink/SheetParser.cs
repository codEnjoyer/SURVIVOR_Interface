using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoogleSheetLink
{
    public class SheetParser : MonoBehaviour
    {
        private readonly Dictionary<string, Func<string[], GameObject, Object[]>> converter = new();

        [SerializeField] private string sheetName;
        [SerializeField] private string from;
        [SerializeField] private string to;

        private GoogleSheetHelper googleSheetHelper;
        private string range;
        private BaseItemDataParser baseItemDataParser;

        private const string Dir = "Assets/Resources/Items";

        private void Awake()
        {
            baseItemDataParser = new BaseItemDataParser();
            googleSheetHelper = new GoogleSheetHelper("12o3fSTiRqjt2EpLmurYA9KE_DWGaghkFuJkT4jzL09g", "JsonKey.json");
            range = $"{sheetName}!{from}:{to}";

            converter.Add("BaseItem", (param, obj) =>
            {
                var baseItem = obj.AddComponent<BaseItem>();
                var baseItemData = baseItemDataParser.Parse(param);
                baseItemData.name = "ItemData";
                baseItem.itemData = baseItemData;
                return new Object[] {baseItemData};
            });
            var table = googleSheetHelper.ReadEntries(range)
                .Select(x => x
                    .Select(y => y.ToString())
                    .ToList())
                .ToList();
            ParseAndSave(table);
        }

        public void ParseAndSave(List<List<string>> table)
        {
            foreach (var row in table)
            {
                var path = row[0];
                var prefabName = row[1];
                var newGameObject = Instantiate(new GameObject());
                var assets = new List<Object>();
                var componentName = "";
                var param = new List<string>();
                
                for (var j = 2; j < row.Count; j++)
                {
                    var cell = row[j];
                    if (cell.Contains("$"))
                    {
                        if (componentName != "")
                            assets.AddRange(converter[componentName](param.ToArray(), newGameObject));
                        
                        componentName = cell.Replace("$", "");
                        param.Clear();
                    }
                    else
                    {
                        param.Add(cell);
                    }
                }
                assets.AddRange(converter[componentName](param.ToArray(), newGameObject));
                SaveGameObject(path, prefabName, newGameObject);
                foreach (var asset in assets)
                    SaveAsset(path, prefabName+asset.name, asset);
            }
        }


        private void SaveGameObject(string path, string prefabName, GameObject gameObject)
        {
            path = AssetDatabase.GenerateUniqueAssetPath($@"{Dir}/{path}/{prefabName}.prefab");
            PrefabUtility.SaveAsPrefabAsset(gameObject, path, out var prefabSuccess);
            if (!prefabSuccess)
            {
                Debug.Log($"Не удалось сохранить префаб {prefabName} в дирректории {path}");
            }
        }

        private void SaveAsset(string path, string assetName, Object asset)
        {
            path = AssetDatabase.GenerateUniqueAssetPath($@"{Dir}/{path}/{assetName}.asset");
            AssetDatabase.CreateAsset(asset, path);
        }
    }
}