using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;
    
    public int Width => width;

    public int Height => height;
}