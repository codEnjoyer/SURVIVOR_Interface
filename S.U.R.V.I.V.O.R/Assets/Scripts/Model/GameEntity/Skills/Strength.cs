using System;
using System.Collections.Generic;

namespace Model.GameEntity.Skills
{
    public class Strength : Skill, IDrawableSkillLevel
    {
        class StrengthLevel
        {
            public int NeededExperienceToLevelUp { get; set; }
            public int AdditionalHp { get; set; }
            public int AdditionalPortableWeight { get; set; }
            public int AdditionalMeleeDamage { get; set; }

            public StrengthLevel(int neededExperienceToLevelUp, int additionalHp, int additionalPortableWeight,
                int additionalMeleeDamage)
            {
                NeededExperienceToLevelUp = NeededExperienceToLevelUp;
                AdditionalHp = additionalHp;
                AdditionalPortableWeight = additionalPortableWeight;
                AdditionalMeleeDamage = additionalMeleeDamage;
            }
        }

        private readonly Body body;
        private const float ExpForDevelopment = 3;

        private readonly Dictionary<int, StrengthLevel> skillCharacteristic = new()
        {
            {1, new StrengthLevel(100, 10, 10, 10)}
            //TODO 
        };

        public Strength(Body body) : base(5)
        {
            this.body = body;
        }

        private StrengthLevel CurrentStrengthLevel => skillCharacteristic[CurrentLevel];
        private StrengthLevel PreviousStrengthLevel => skillCharacteristic[CurrentLevel - 1];
        public int AdditionalMeleeDamage => CurrentStrengthLevel.AdditionalMeleeDamage;

        public override void Development()
        {
            LevelProgress += ExpForDevelopment / CurrentStrengthLevel.NeededExperienceToLevelUp;
            if (IsFinishLevelProgress)
            {
                LevelProgress = 0;
                CurrentLevel += 1;
                StrengthUpped();
            }
        }

        private void StrengthUpped()
        {
            foreach (var bodyPart in body.BodyParts)
            {
                var addHp = CurrentLevel == 1
                    ? CurrentStrengthLevel.AdditionalHp
                    : CurrentStrengthLevel.AdditionalHp - PreviousStrengthLevel.AdditionalHp;
                bodyPart.MaxHp += addHp;
            }
        }

        public string GetLevelInformation()
        {
            throw new NotImplementedException();
        }
    }
}