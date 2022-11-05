using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 50)]
public class BaseItemData : ScriptableObject
{
    [SerializeField] private Size size;
    [SerializeField] private Sprite icon;
    [SerializeField] private string description;
    [SerializeField] private float weight;

    public Size Size => size;
    public Sprite Icon => icon;
    public string Description => description;
    public float Weight => weight;
}