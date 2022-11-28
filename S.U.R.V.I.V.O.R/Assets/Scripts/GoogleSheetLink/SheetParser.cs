using System;
using UnityEngine;

namespace GoogleSheetLink
{
    public class SheetParser: MonoBehaviour
    {
        [SerializeField] private string sheetName;
        [SerializeField] private string from;
        [SerializeField] private string to;
        private const string SpreadsheetId = "12o3fSTiRqjt2EpLmurYA9KE_DWGaghkFuJkT4jzL09g";
        private const string SecretJsonFileName = "JsonKey.json";
        private GoogleSheetHelper googleSheetHelper;
        private string range;

        private void Awake()
        {
            googleSheetHelper = new GoogleSheetHelper(SpreadsheetId, SecretJsonFileName);
            range = $"{sheetName}!{from}:{to}";
        }
    }
}