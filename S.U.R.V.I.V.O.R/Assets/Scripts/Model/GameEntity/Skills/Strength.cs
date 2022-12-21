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

        private readonly Dictionary<int, StrengthLevel> skillCharacteristic = new()
        {
            {1, new StrengthLevel(100, 10, 10, 10)}
            //TODO 
        };

        public Strength(Body body) : base(5)
        {
            this.body = body;
        }

        public string GetLevelInformation()
        {
            throw new NotImplementedException();
        }

        public override void Development()
        {
            throw new NotImplementedException();
        }
    }
}