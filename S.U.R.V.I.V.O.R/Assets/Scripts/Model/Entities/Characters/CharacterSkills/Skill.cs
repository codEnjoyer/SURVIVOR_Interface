using System;
using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Entities.Characters.CharacterSkills
{
    public abstract class Skill : IDeveloping, ISaved<SkillSave>
    {
        [SerializeField] [Min(1)] private int maxLevel;
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
        public SkillSave CreateSave()
        {
            return new SkillSave()
            {
                currentLevel = CurrentLevel,
                levelProgress = LevelProgress
            };
        }

        public void Restore(SkillSave save)
        {
            CurrentLevel = save.currentLevel;
            LevelProgress = save.levelProgress;
        }
    }

    [DataContract]
    public class SkillSave
    {
        [DataMember] public int currentLevel;
        [DataMember] public float levelProgress;
    }
}