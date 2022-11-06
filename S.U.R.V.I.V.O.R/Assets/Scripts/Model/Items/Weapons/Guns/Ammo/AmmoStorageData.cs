using UnityEngine;


[CreateAssetMenu(fileName = "New AmmoStorageData", menuName = "Data/AmmoStorage Data", order = 50)]
public class AmmoStorageData: ScriptableObject
{
    [SerializeField] private Bullet caliberType;
    [SerializeField] private int capacity;

    public Bullet CaliberType => caliberType;

    public int Capacity => capacity;
}