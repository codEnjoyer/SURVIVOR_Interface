using UnityEngine;

public class TestScriptableObject: ScriptableObject
{
    public int x;
    public int y;

    public TestScriptableObject(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}