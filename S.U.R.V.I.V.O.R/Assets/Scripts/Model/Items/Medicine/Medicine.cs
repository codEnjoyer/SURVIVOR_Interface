using System.Collections.Generic;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Medicine : MonoBehaviour
{
    [SerializeField] private List<HealthPropertyType> targets;
}

