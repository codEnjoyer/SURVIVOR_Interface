using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagazineData", menuName = "Data/Magazine Data", order = 50)]
public class MagazineData : ScriptableObject
{
    [SerializeField] private Caliber caliber;

    public Caliber Caliber => caliber;
}
