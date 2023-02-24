using System.Collections;
using System.Collections.Generic;
using Model;
using Model.SaveSystem;
using Scenes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSave : MonoBehaviour
{
    [SerializeField] private string saveName;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!string.IsNullOrEmpty(saveName))
        {
            var path = Application.persistentDataPath +"\\"+saveName + ".dat";
            var save = SaveManager.ReadObject<GlobalMapData>(path);
            GlobalMapController.Data = save;
        }
        SceneTransition.LoadScene(SceneName.GlobalMapScene);
    }
}
