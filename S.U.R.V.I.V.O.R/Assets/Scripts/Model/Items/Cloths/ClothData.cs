using UnityEngine;

[CreateAssetMenu(fileName = "New ClothData", menuName = "Data/Cloth Data", order = 50)]
public class ClothData : ScriptableObject
{
    [SerializeField] private int maxArmor;
    [SerializeField] private Size inventorySize;
    [SerializeField] private int warm;
    [SerializeField] private ClothType clothType;
    
    public int MaxArmor => maxArmor;
    public Size InventorySize => inventorySize;
    public int Warm => warm;
    public ClothType ClothType => clothType;
}