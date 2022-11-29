using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoogleSheetLink
{
    public class SizeParser
    {
        private readonly string absolutePath;
        private readonly string relativePath;
        private readonly Regex regex;
        private List<string> sizeObjectsFileNames;

        public SizeParser()
        {
            regex = new Regex(@"\d+");
            absolutePath = $@"{Application.dataPath}/Resources/InventorySizeObjects";
            relativePath = @"Assets/Resources/InventorySizeObjects";
            FindSizeObjects();
        }

        public Size Parse(string sizeObjectName)
        {
            var sizeObjectFileName = $"{sizeObjectName}.asset";
            var objPath = $@"{absolutePath}\{sizeObjectFileName}";
            if (sizeObjectsFileNames.Any(path => string.CompareOrdinal(path, objPath) == 0))
            {
                var a = Resources.Load<Size>($@"InventorySizeObjects/{sizeObjectName}");
                Debug.Log(a);
                return a;
            }
            else
                return CreateSizeObject(sizeObjectFileName);
        }

        private Size CreateSizeObject(string sizeObjectName)
        {
            var findData = regex.Matches(sizeObjectName)
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            if (findData.Length != 2)
                throw new ArgumentException($"Неверное форматирование размера предмета ({sizeObjectName}). Объект не был создан.");
            var sizeObj = Object.Instantiate(new Size(findData[0], findData[1]));

            var objPath = AssetDatabase.GenerateUniqueAssetPath($"{relativePath}/{sizeObjectName}");
            AssetDatabase.CreateAsset(sizeObj, objPath);
            return sizeObj;
        }

        private void FindSizeObjects()
        {
            sizeObjectsFileNames = new List<string>();
            var regex = new Regex(@"[.]asset\z");
            foreach (var file in Directory.EnumerateFiles(absolutePath))
                if(regex.IsMatch(file))
                    sizeObjectsFileNames.Add(file);
        }
    }
}