using System;
using System.Data;

namespace Model.GameEntity.Skills
{
    public abstract class Skill : ISkill
    {
        protected abstract class BaseLevel
        {
            public int necessaryExperienceToLevelUp;

            public BaseLevel(int necessaryExperienceToLevelUp)
            {
                this.necessaryExperienceToLevelUp = necessaryExperienceToLevelUp;
            }
        }

        private int maxLevel;
        public string Name { get; private set; }
        public string Description { get; private set; }

        public int MaxLevel
        {
            get => maxLevel;
            set
            {
                if (value <= 0)
                    throw new ConstraintException("Максимальный уровень скила не может быть ниже нуля!");
                maxLevel = value;
            }
        }

        public Skill(int maxLevel, string name = "Skill", string description = "Description")
        {
            Name = name;
            Description = description;
            MaxLevel = maxLevel;
        }

        public abstract void AddExperience();
    }
}