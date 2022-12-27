using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Cookable))]
public class CookableFood : EatableFood
{
    [SerializeField] private List<GameObject> objectToSpawnAfterCook;

    public IEnumerable<GameObject> ObjectToSpawnAfterCook => objectToSpawnAfterCook;
}
