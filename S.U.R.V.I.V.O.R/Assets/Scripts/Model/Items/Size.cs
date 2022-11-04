using UnityEngine;

[CreateAssetMenu(fileName = "New Size", menuName = "Data/Size", order = 50)]
public class Size : ScriptableObject
{
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;
    
    public int Width => width;

    public int Height => height;
}