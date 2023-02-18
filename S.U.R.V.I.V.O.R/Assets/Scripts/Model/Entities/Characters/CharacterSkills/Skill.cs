using System;
using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Entities.Characters.CharacterSkills
{
    public abstract class Skill : IDeveloping, ISaved<SkillData>
    {
        private int maxLevel;
        private int currentLevel = 0;
        private float levelProgress = 0;
        public string Name { get; }
        public string Description { get;  }

        public Skill(int maxLevel, string name = "Skill", string description = "Description")
        {
            Name = name;
            Description = description;
            MaxLevel = maxLevel;
            CurrentLevel = 0;
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
            protected set => currentLevel = Math.Min(MaxLevel, Math.Max(0, value));
        }

        public float LevelProgress
        {
            get => levelProgress;
            set => levelProgress = Math.Min(1, Math.Max(0, value));
        }

        public bool IsFinishLevelProgress => Math.Abs(LevelProgress - 1) < 0.000001;


        public abstract void Development();
        public virtual SkillData CreateData()
        {
            return new SkillData()
            {
                currentLevel = CurrentLevel,
                levelProgress = LevelProgress
            };
        }

        public virtual void Restore(SkillData data)
        {
            CurrentLevel = data.currentLevel;
            LevelProgress = data.levelProgress;
        }
    }

    [DataContract]
    public class SkillData
    {
        [DataMember] public int currentLevel;
        [DataMember] public float levelProgress;
    }
}