using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagazineData", menuName = "Data/Magazine Data", order = 50)]
public class MagazineData : ScriptableObject
{
    [SerializeField] private Caliber caliber;
    [SerializeField] private int maxAmmoAmount;

    public Caliber Caliber => caliber;

    public int MaxAmmoAmount => maxAmmoAmount;
}
