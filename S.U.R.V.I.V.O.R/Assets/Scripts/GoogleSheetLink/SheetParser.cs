using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoogleSheetLink.DataParsers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoogleSheetLink
{
    public class SheetParser : MonoBehaviour
    {
        private readonly Dictionary<string, Func<string, string[], GameObject, Task<Object[]>>> converter = new();

        [SerializeField] private string sheetName;
        [SerializeField] private string from;
        [SerializeField] private string to;

        private GoogleSheetHelper googleSheetHelper;
        private string range;
        private string relativePath;
        private string absolutePath;

        private async void Awake()
        {
            relativePath = "Assets/Resources/Items";
            absolutePath = $@"{Application.dataPath}/Resources/Items";
            googleSheetHelper = new GoogleSheetHelper("12o3fSTiRqjt2EpLmurYA9KE_DWGaghkFuJkT4jzL09g", "JsonKey.json");
            range = $"{sheetName}!{from}:{to}";

            converter.Add("BaseItem", async (fullComponentName, param, obj) =>
            {
                var baseItem = obj.AddComponent<BaseItem>();
                var baseItemData = await BaseItemDataParser.Parse(param);
                baseItem.SetBaseItemData(baseItemData);
                return new Object[] {baseItemData, baseItemData.Icon};
            });

            converter.Add("Gun", (fullComponentName, param, obj) =>
            {
                var gunClassName = fullComponentName.Split("=>")[1];
                var gun = obj.AddComponent(Type.GetType(gunClassName)) as Gun;
                var gunData = GunDataParser.Parse(param);
                if (gun == null)
                    throw new Exception($"Типа {gunClassName} не существует");
                gun.Data = gunData;
                return Task.FromResult(new Object[] {gunData});
            });


            var table = googleSheetHelper.ReadEntries(range)
                .Select(x => x
                    .Select(y => y.ToString())
                    .ToList())
                .ToList();
            await ParseAndSave(table);
        }

        public async Task ParseAndSave(List<List<string>> table)
        {
            foreach (var row in table)
            {
                var path = row[0];
                var prefabName = row[1];
                var newGameObject = Instantiate(new GameObject());
                var assets = new List<Object>();
                var fullComponentName = "";
                var param = new List<string>();
                string mainComponentName;
                
                for (var j = 2; j < row.Count; j++)
                {
                    var cell = row[j];
                    if (cell.Contains("$"))
                    {
                        if (fullComponentName != "")
                        {
                            mainComponentName = fullComponentName.Split("=>")[0];
                            assets.AddRange(await converter[mainComponentName](fullComponentName, param.ToArray(), newGameObject));
                        }

                        fullComponentName = cell.Replace("$", "");
                        param.Clear();
                    }
                    else
                        param.Add(cell);
                }

                mainComponentName = fullComponentName.Split("=>")[0];
                assets.AddRange(await converter[mainComponentName](fullComponentName, param.ToArray(), newGameObject));
                CreateDirectoryForPrefab(path);
                foreach (var asset in assets)
                    SaveAsset(path, prefabName + asset.name, asset);
                SaveGameObject(path, prefabName, newGameObject);
            }
        }

        private bool CheckCreateComponents(Action func)
        {
            try
            {
                func();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }

            return true;
        }

        private void SaveGameObject(string path, string prefabName, GameObject prefab)
        {
            var absoluteFilePath = $@"{absolutePath}/{path}/{prefabName}.prefab";
            if (File.Exists(absoluteFilePath))
            {
                Debug.Log($"Префаб {prefabName} уже существует");
                return;
            }

            path = AssetDatabase.GenerateUniqueAssetPath($@"{relativePath}/{path}/{prefabName}.prefab");
            PrefabUtility.SaveAsPrefabAsset(prefab, path, out var prefabSuccess);
            if (!prefabSuccess)
            {
                Debug.Log($"Не удалось сохранить префаб {prefabName} в дирректории {path}");
            }
        }

        private void SaveAsset(string path, string assetName, Object asset)
        {
            var absoluteFilePath = $@"{absolutePath}/{path}/{assetName}.asset";
            if (File.Exists(absoluteFilePath))
            {
                Debug.Log($"Ассет {assetName} уже существует");
                return;
            }

            path = AssetDatabase.GenerateUniqueAssetPath($@"{relativePath}/{path}/{assetName}.asset");
            AssetDatabase.CreateAsset(asset, path);
        }

        private void CreateDirectoryForPrefab(string path)
        {
            var term = absolutePath;
            var splitPath = path.Split();
            foreach (var dir in splitPath)
            {
                term += $@"/{dir}";
                if (!Directory.Exists(term))
                {
                    Directory.CreateDirectory(term);
                    Debug.Log($"Была создана дирректория {term}");
                }
            }
        }
    }
}