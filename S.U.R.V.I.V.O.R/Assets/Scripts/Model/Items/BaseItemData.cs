using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/Item Data", order = 50)]
public sealed class BaseItemData : ScriptableObject
{
    [FormerlySerializedAs("name")]
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Size size;
    [SerializeField] private Sprite icon;
    [SerializeField] private float weight;

    public BaseItemData(string itemName, string description, Size size, Sprite icon, float weight)
    {
        this.itemName = itemName;
        this.description = description;
        this.size = size;
        this.icon = icon;
        this.weight = weight;
    }

    public string ItemName => itemName;
    public Size Size => size;
    public Sprite Icon => icon;
    public string Description => description;
    public float Weight => weight;
}