using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace GoogleSheetLink
{
    public class SizeParser
    {
        private readonly string absolutePath;
        private readonly string relativePath;
        private readonly List<Size> sizeObjects;


        public SizeParser()
        {
            absolutePath = $@"{Application.dataPath}/Resources/InventorySizeObjects";
            relativePath = @"Assets/Resources/InventorySizeObjects";
            sizeObjects = FindSizeObjects();
        }

        public Size Parse(string sizeObjectName)
        {
            var newSizeObj = ConvertToSize(sizeObjectName);
            if (sizeObjects.Any(size => newSizeObj.Equals(size)))
                return Resources.Load<Size>($@"InventorySizeObjects/{sizeObjectName}");
            return CreateSizeObject(newSizeObj, sizeObjectName);
        }

        private Size CreateSizeObject(Size sizeObj, string sizeObjectName)
        {
            var objPath = AssetDatabase.GenerateUniqueAssetPath($"{relativePath}/{sizeObjectName}.asset");
            AssetDatabase.CreateAsset(sizeObj, objPath);
            Debug.Log($"Объект {nameof(Size)} с именем {sizeObjectName} не был найден," +
                      $" объект был создан автоматически");
            return sizeObj;
        }

        private List<Size> FindSizeObjects() => FindSizeObjectsNames()
            .Select(name => Resources.Load<Size>($@"InventorySizeObjects/{name}"))
            .ToList();


        private List<string> FindSizeObjectsNames()
        {
            var pattern = new Regex(@"[.]asset\z");
            return Directory.GetFiles(absolutePath)
                .Where(file => pattern.IsMatch(file))
                .Select(file => Path.GetFileName(file).Split(".")[0])
                .ToList();
        }

        private Size ConvertToSize(string sizeObjectName)
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