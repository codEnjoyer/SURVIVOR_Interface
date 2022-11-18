using System;

public class Skill
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int MaxLevel { get; private set; }
    public int Level { get; private set; }
    

    public event Action<int> OnLevelUp;
    public Skill(int maxLevel, string name = "Skill", string description = "Description", int curLevel = 1)
    {
        Name = name;
        Description = description;
        MaxLevel = maxLevel;
        Level = curLevel;
    }
    public void LevelUp()
    {
        if(Level < MaxLevel)
        {
            Level++;
            OnLevelUp?.Invoke(Level);
        }
    }
}