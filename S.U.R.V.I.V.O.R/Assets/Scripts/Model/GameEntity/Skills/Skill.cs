using System;
using System.Collections;
using System.Data;

namespace Model.GameEntity.Skills
{
    public abstract class Skill : IDeveloping
    {
        private int maxLevel;
        private int currentLevel;
        private float levelProgress;
        public string Name { get; }
        public string Description { get; }

        public Skill(int maxLevel, string name = "Skill", string description = "Description")
        {
            Name = name;
            Description = description;
            MaxLevel = maxLevel;
        }

        public int MaxLevel
        {
            get => maxLevel;
            private set
            {
                maxLevel = Math.Max(1, value);
                currentLevel = Math.Min(MaxLevel, currentLevel);
            }
        }

        public int CurrentLevel
        {
            get => currentLevel;
            protected set => currentLevel = Math.Min(MaxLevel, Math.Max(1, value));
        }

        public float LevelProgress
        {
            get => levelProgress;
            set => levelProgress = Math.Min(0, Math.Max(1, value));
        }


        public abstract void Development();
    }
}