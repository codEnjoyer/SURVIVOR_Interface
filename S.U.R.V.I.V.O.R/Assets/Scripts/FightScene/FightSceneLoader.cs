using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class FightSceneLoader
{
    public static FightData CurrentData {get; private set;}
    public static void Load(SceneName sceneName)
    {
        SceneManager.LoadScene((int) sceneName, LoadSceneMode.Single);
    }

    public static void SendDataToLoader(FightData data) => CurrentData = data;
}
