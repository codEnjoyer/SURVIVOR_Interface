using UnityEngine;

[CreateAssetMenu(fileName = "New AmmoContainerData", menuName = "Data/AmmoContainer Data", order = 50)]
public class AmmoContainerData : ScriptableObject
{
    [SerializeField] private SingleAmmo ammoType;
    
    [SerializeField] private int capacity;

    public SingleAmmo AmmoType => ammoType;

    public int Capacity => capacity;

    public Caliber Caliber => ammoType.Caliber;
    
    
}