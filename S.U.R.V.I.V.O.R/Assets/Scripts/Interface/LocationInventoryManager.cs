using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class LocationInventoryManager : MonoBehaviour
{
    public Group group;
    [SerializeField] private Text text;

    public void Start()
    {
        group.GetComponent<GroupMovementLogic>().LocationCahnge += LocationChanged;
    }

    private void LocationChanged(string value)
    {
        text.text = value;
    }
}
