using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PackedContainerData", menuName = "Data/Packed Container Data", order = 50)]
public class PackedContainerData : ScriptableObject
{
    [SerializeField] private List<GameObject> packedItems;

    public IEnumerable<GameObject> PackedItems => packedItems;
}
