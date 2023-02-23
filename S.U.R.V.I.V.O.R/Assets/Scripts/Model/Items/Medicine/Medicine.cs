using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Medicine : MonoBehaviour
{
    [SerializeField] private List<HealthPropertyType> targets;
}

