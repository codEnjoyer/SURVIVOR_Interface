using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;
    public float weight = 1;

    public Sprite itemIcon;
}