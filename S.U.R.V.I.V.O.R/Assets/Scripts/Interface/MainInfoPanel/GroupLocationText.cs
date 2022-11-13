using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupLocationText : MonoBehaviour
{
    private InterfaceGruopLogicController interfaceController;

    public void Awake()
    {
        interfaceController = FindObjectOfType(typeof(InterfaceGruopLogicController)) as InterfaceGruopLogicController;
        GetComponent<Text>().text = interfaceController.currentGroup.location.Data.LocationName;
    }

    public void Update()
    {
        var text = GetComponent<Text>();
        if (text.text != interfaceController.currentGroup.location.Data.LocationName)
        {
            text.text = interfaceController.currentGroup.location.Data.LocationName;
        }
    }
}
