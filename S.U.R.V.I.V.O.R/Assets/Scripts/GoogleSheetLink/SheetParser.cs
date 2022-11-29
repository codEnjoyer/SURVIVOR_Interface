using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GoogleSheetLink
{
    public class SheetParser: MonoBehaviour
    {
        // private Dictionary<string, Func<object[], object>> сonverter = new()
        // {
        //     {"BaseItem", param => }
        // };
        [SerializeField] private string sheetName;
        [SerializeField] private string from;
        [SerializeField] private string to;
        
        private GoogleSheetHelper googleSheetHelper;
        private string range;


        private void Awake()
        {

            googleSheetHelper = new GoogleSheetHelper("12o3fSTiRqjt2EpLmurYA9KE_DWGaghkFuJkT4jzL09g", "JsonKey.json");
            range = $"{sheetName}!{from}:{to}";
        }

        private void Parse(List<List<string>> table)
        {
            foreach (var row in table)
            {
                foreach (var cell in row)
                {
                    
                }
            }
        }
    }
}