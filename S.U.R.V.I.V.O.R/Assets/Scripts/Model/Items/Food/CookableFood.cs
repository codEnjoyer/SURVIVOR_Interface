using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Cookable))]
public class CookableFood : EatableFood
{
    [SerializeField] private List<BaseItem> objectToSpawnAfterCook;

    public IEnumerable<BaseItem> ObjectToSpawnAfterCook => objectToSpawnAfterCook;

    public IEnumerable<BaseItem> Cook()
    {
        Destroy(gameObject);
        return objectToSpawnAfterCook;
    }
}
