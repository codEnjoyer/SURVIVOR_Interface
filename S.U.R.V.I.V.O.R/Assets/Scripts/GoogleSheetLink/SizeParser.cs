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
            sizeObjectName = $"{sizeObjectName}.asset";
            var objPath = $@"{absolutePath}/{sizeObjectName}";
            if (sizeObjectsFileNames.Contains(sizeObjectName))
                return (Size) Resources.Load(objPath);
            else
                return CreateSizeObject(sizeObjectName);
        }

        private Size CreateSizeObject(string sizeObjectName)
        {
            var findData = regex.Matches(sizeObjectName)
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            if (findData.Length != 2)
                throw new ArgumentException("Неверное форматирование размера предмета (Size). Объект не был создан.");
            var sizeObj = new Size(findData[0], findData[1]);

            var objPath = AssetDatabase.GenerateUniqueAssetPath(relativePath);
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