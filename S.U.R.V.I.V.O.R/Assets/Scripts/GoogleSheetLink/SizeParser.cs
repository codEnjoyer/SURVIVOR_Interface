using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace GoogleSheetLink
{
    public static class SizeParser
    {
        private static readonly string absolutePath;
        private static readonly string relativePath;
        private static readonly List<Size> sizeObjects;


        static SizeParser()
        {
            absolutePath = $@"{Application.dataPath}/Resources/InventorySizeObjects";
            relativePath = @"Assets/Resources/InventorySizeObjects";
            sizeObjects = FindSizeObjects();
        }

        public static Size Parse(string sizeObjectName)
        {
            var newSizeObj = ConvertToSize(sizeObjectName);
            if (sizeObjects.Any(size => newSizeObj.Equals(size)))
                return Resources.Load<Size>($@"InventorySizeObjects/{sizeObjectName}");
            return CreateSizeObject(newSizeObj, sizeObjectName);
        }

        private static Size CreateSizeObject(Size sizeObj, string sizeObjectName)
        {
            var objPath = AssetDatabase.GenerateUniqueAssetPath($"{relativePath}/{sizeObjectName}.asset");
            AssetDatabase.CreateAsset(sizeObj, objPath);
            Debug.Log($"Объект {nameof(Size)} с именем {sizeObjectName} не был найден," +
                      $" объект был создан автоматически");
            return sizeObj;
        }

        private static List<Size> FindSizeObjects() => FindSizeObjectsNames()
            .Select(name => Resources.Load<Size>($@"InventorySizeObjects/{name}"))
            .ToList();


        private static List<string> FindSizeObjectsNames()
        {
            var pattern = new Regex(@"[.]asset\z");
            return Directory.GetFiles(absolutePath)
                .Where(file => pattern.IsMatch(file))
                .Select(file => Path.GetFileName(file).Split(".")[0])
                .ToList();
        }

        private static Size ConvertToSize(string sizeObjectName)
        {
            var pattern = new Regex(@"\d+");
            var findData = pattern.Matches(sizeObjectName)
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            if (findData.Length != 2)
                throw new ArgumentException(
                    $"Неверное форматирование размера предмета ({sizeObjectName}). Объект не был создан.");

            return new Size(findData[0], findData[1]);
        }
    }
}