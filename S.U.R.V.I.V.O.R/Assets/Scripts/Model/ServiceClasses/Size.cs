using System;
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

    public override bool Equals(object other)
    {
        if (other is Size otherSize)
            return Equals(otherSize);
        return false;
    }

    private bool Equals(Size other) => width == other.width && height == other.height;
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), width, height);
}