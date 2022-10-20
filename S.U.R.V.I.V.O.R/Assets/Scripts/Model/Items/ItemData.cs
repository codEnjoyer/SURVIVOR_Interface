using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;
    public float weight = 1;

    public Sprite itemIcon;
}