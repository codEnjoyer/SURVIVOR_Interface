using UnityEngine;

[CreateAssetMenu(fileName = "New Size", menuName = "Data/Size", order = 50)]
public class Size : ScriptableObject
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    public Size(int width = 1, int height = 1)
    {
        this.width = width;
        this.height = height;
    }

    public int Width => width;

    public int Height => height;
}