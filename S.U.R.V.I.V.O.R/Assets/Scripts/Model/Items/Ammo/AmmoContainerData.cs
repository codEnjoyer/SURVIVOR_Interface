using UnityEngine;

[CreateAssetMenu(fileName = "New AmmoContainerData", menuName = "Data/AmmoContainer Data", order = 50)]
public class AmmoContainerData:ScriptableObject
{
    [SerializeField] private Caliber caliberType;
    [SerializeField] private int capacity;

    public Caliber CaliberType => caliberType;

    public int Capacity => capacity;
}