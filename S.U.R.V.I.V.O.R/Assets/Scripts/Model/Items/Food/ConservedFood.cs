using System.Collections;
using System.Collections.Generic;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(Conserved))]
public class ConservedFood : MonoBehaviour
{
    [SerializeField] private ConservedFoodData data;

    public BaseItem Open()
    {
        GetComponent<BaseItem>().Destroy();
        return data.ItemToSpawnAfterConserveOpen;
    }
}