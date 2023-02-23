#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;

namespace GoogleSheetLink
{
    public class SheetParser : MonoBehaviour
    {
        private const string LIST_SEPARATOR = ", ";

        [SerializeField] private string sheetName;
        [SerializeField] private string from;
        [SerializeField] private string to;

        private GoogleSheetHelper googleSheetHelper;
        private string range;
        private string relativePath;
        private string absolutePath;

        private void Awake()
        {
            if (string.IsNullOrEmpty(sheetName)
                || string.IsNullOrEmpty(from)
                || string.IsNullOrEmpty(to))
                return;
            relativePath = "Assets/Resources/Items";
            absolutePath = $@"{Application.dataPath}/Resources/Items";
            googleSheetHelper = new GoogleSheetHelper("12o3fSTiRqjt2EpLmurYA9KE_DWGaghkFuJkT4jzL09g",
                "JsonKey.json");
            range = $"{sheetName}!{from}:{to}";

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
                var newGameObject = new GameObject();
                var assets = new List<Object>();
                var param = new List<string>();
                string fullComponentName = null;
                var continueFlag = false;

                for (var j = 2; j < row.Count; j++)
                {
                    var cell = row[j];
                    if (cell.Contains("$"))
                    {
                        if (fullComponentName != null)
                        {
                            if (CreateAndCheckComponents() == false)
                            {
                                continueFlag = true;
                                break;
                            }
                        }

                        fullComponentName = cell.Replace("$", "");
                        param.Clear();
                    }
                    else
                        param.Add(cell);
                }

                if (continueFlag || CreateAndCheckComponents() == false)
                    continue;
                CreateDirectoryForPrefab(path);
                foreach (var asset in assets)
                    SaveAsset(path, prefabName + asset.name, asset);
                SaveGameObject(path, prefabName, newGameObject);

                bool CreateAndCheckComponents()
                {
                    try
                    {
                        var asset = AddComponent(newGameObject, fullComponentName, param.ToArray());
                        assets.Add(asset);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        return false;
                    }

                    return true;
                }
            }
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

        private Object AddComponent(GameObject obj, string fullComponentName, string[] param)
        {
            var tempSplit = fullComponentName.Split("=>");
            var mainComponentName = tempSplit[0];
            var componentName = tempSplit[^1];

            var componentType = Type.GetType(componentName);
            if (componentType == null)
                throw new Exception(@$"Указанный компонент {componentName} не был найден");

            var componentDataType = Type.GetType(componentName + "Data");
            if (componentDataType == null)
            {
                componentDataType = Type.GetType(mainComponentName + "Data");
                if (componentDataType == null)
                    throw new Exception($"Не удалось найти тип data поля! {fullComponentName}");
            }

            var componentData = CreateDataObject(componentDataType, param);
            var componentDataField = componentType.GetField("data", BindingFlags.Instance | BindingFlags.NonPublic);
            if (componentDataField == null)
                throw new Exception(
                    @$"Не найдено data поле. Убедитесь, что указанный компонент содержит приватное поле data");

            var component = obj.AddComponent(componentType);
            componentDataField.SetValue(component, componentData);
            return (Object) componentData;
        }


        private object CreateDataObject(Type type, string[] param)
        {
            var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfos.Length != param.Length)
                throw new Exception($"Для типа {type} было переданно неверное колличество данных! ({fieldInfos.Length} != {param.Length})");
            var obj = ScriptableObject.CreateInstance(type);
            for (var i = 0; i < fieldInfos.Length; i++)
            {
                var dataForField = param[i];
                if (string.IsNullOrEmpty(dataForField))
                    continue;
                var fieldInfo = fieldInfos[i];
                if (fieldInfo.FieldType == typeof(string))
                    fieldInfo.SetValue(obj, dataForField);
                else
                {
                    var objForField = GetObjectForField(fieldInfo.FieldType, dataForField);
                    fieldInfo.SetValue(obj, objForField);
                }
            }

            return obj;
        }

        private object GetObjectForField(Type fieldType, string dataForField)
        {
            if (fieldType.GetInterface(nameof(ICollection)) != null)
            {
                var elemType = fieldType.GetGenericArguments().First();
                var collection = Activator.CreateInstance(fieldType);
                var addMethod = fieldType.GetMethod("Add", new[] {elemType});
                if (addMethod == null)
                    throw new Exception($"Тип {fieldType} не реализует метод Add!");
                foreach (var s in dataForField.Split(LIST_SEPARATOR))
                {
                    var elemObj = GetObjectForField(elemType, s);
                    addMethod.Invoke(collection, new object[] {elemObj});
                }

                return collection;
            }

            if (fieldType.IsEnum)
                return Enum.Parse(fieldType, dataForField);

            var parseMethod = fieldType.GetMethod("Parse", new[] {typeof(string)});
            if (parseMethod == null)
                throw new Exception($"Для типа {fieldType} не найден статический публичный метод Parse!");
            return parseMethod.Invoke(null, new object[] {dataForField});
        }
    }
}
#endif