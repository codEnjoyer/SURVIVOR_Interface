using UnityEngine;

[CreateAssetMenu(fileName = "New ClothesData", menuName = "Data/Clothes Data", order = 50)]
public class ClothesData: ScriptableObject
{
    [SerializeField] private int maxArmor;
    [SerializeField] private Size inventorySize;

    public int MaxArmor => maxArmor;
    public Size InventorySize => inventorySize;
}