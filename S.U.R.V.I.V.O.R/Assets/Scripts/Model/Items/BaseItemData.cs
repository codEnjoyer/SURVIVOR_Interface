using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/Item Data", order = 50)]
public class BaseItemData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Size size;
    [SerializeField] private Sprite icon;
    [SerializeField] private float weight;

    public BaseItemData(string name, string description, Size size, Sprite icon, float weight)
    {
        this.name = name;
        this.description = description;
        this.size = size;
        this.icon = icon;
        this.weight = weight;
    }

    public string Name => name;
    public Size Size => size;
    public Sprite Icon => icon;
    public string Description => description;
    public float Weight => weight;
}